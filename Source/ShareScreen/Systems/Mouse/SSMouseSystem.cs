using System;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Runtime.InteropServices;
using SS.ShareScreen.Core.Payload;
using SS.ShareScreen.Core.Systems;
using SS.ShareScreen.Enums;
using SS.ShareScreen.Extensions;
using SS.ShareScreen.InteractionProviders;
using SS.ShareScreen.Interfaces.System;
using SS.ShareScreen.Windows;
using Color = System.Drawing.Color;
using Pen = System.Drawing.Pen;
using Point = System.Windows;


namespace SS.ShareScreen.Systems.Mouse
{
    [Export(typeof(ISSMouseSystem))]
    public class SSMouseSystem : SSBaseHookSystem, ISSMouseSystem
    {

        private readonly SSWindowsFunctions.LowLevelMouseProc _proc = MouseHookProc;
        private static IntPtr _selectedWindow;
        private static IntPtr _oldHwnd;
        private static eScreenshotType _screenshotType = eScreenshotType.None;
        
        private static readonly IntPtr _higlightPen = SSWindowsFunctions.CreatePen(SSWindowsFunctions.PenStyle.PS_SOLID,3,(uint)ColorTranslator.ToWin32(Color.Red));
        
        [ImportingConstructor]
        public SSMouseSystem()
            : base()
        {
        }

        public Tuple<Point.Point, Point.Point> GetSelectedArea()
        {
            return default(Tuple<Point.Point, Point.Point>);
        }

        public IntPtr GetSelectedWindow()
        {
            return _selectedWindow;
        }

        public void RunSelectingWindow()
        {
            _screenshotType = eScreenshotType.SelectedWindow;
        }

        public void RunSelectingArea()
        {
            _screenshotType = eScreenshotType.SelectedArea;
        }

        public void ResetCurrentAction()
        {
            _screenshotType = eScreenshotType.None;
        }

        protected override Delegate GetCallback() => _proc;

        protected override int GetHookType() => SSWindowsFunctions.WH_MOUSE_LL;

        #region [private]

        private static IntPtr MouseHookProc(int nCode, int wParam, ref SSWindowsFunctions.MouseHookStruct lParam)
        {

            if (_oldHwnd != IntPtr.Zero)
            {
                RefreshWindow(_oldHwnd);
                _oldHwnd = IntPtr.Zero;
            }

            if (nCode >= 0)
            {
                switch (_screenshotType)
                {
                    case eScreenshotType.SelectedWindow:
                        var pt = new SSWindowsFunctions.POINT() {x= lParam.pt.x, y = lParam.pt.y};
                        _selectedWindow = SSWindowsFunctions.WindowFromPoint(pt);
                        if (_selectedWindow != IntPtr.Zero)
                        {
                            HighlightingCurrentWindow(_selectedWindow);
                            ProcessSelectingWindow(wParam);
                            _oldHwnd = _selectedWindow;
                        }
                        
                        break;
                    case eScreenshotType.SelectedArea:
                        break;
                }
            }
            return SSWindowsFunctions.CallNextHookEx(GetHookSystem().GetHookPtr(), nCode, wParam, SSWindowsFunctions.StructToPtr(lParam));
        }

        private static void ProcessSelectingWindow(int wParam)
        {
            if (wParam == SSWindowsFunctions.WM_LBUTTONDOWN)
            {
                ((SSBaseHookSystem) GetHookSystem()).InteractionManager.GetCommand<SSSelectedWindowProvider>()
                    .Publish(new SSPayload<bool>(true));
            }
            else if (wParam == SSWindowsFunctions.WM_RBUTTONDOWN)
            {
                ((SSBaseHookSystem) GetHookSystem()).InteractionManager.GetCommand<SSSelectedWindowProvider>()
                    .Publish(new SSPayload<bool>(false));
                _screenshotType = eScreenshotType.None;
            }
        }

        private static void HighlightingCurrentWindow(IntPtr hWnd)
        {
            SSWindowsFunctions.RECT rc = new SSWindowsFunctions.RECT();

            SSWindowsFunctions.GetWindowRect(hWnd, out rc);

            IntPtr hPrevPen = IntPtr.Zero;
            IntPtr hPrevBrush = IntPtr.Zero;
            
            var hDC = SSWindowsFunctions.GetWindowDC(hWnd);

             

            if (hDC != IntPtr.Zero)
            {
                hPrevPen = SSWindowsFunctions.SelectObject(hDC, _higlightPen);

                hPrevBrush = SSWindowsFunctions.SelectObject(hDC,
                    SSWindowsFunctions.GetStockObject(SSWindowsFunctions.StockObjects.HOLLOW_BRUSH));

                SSWindowsFunctions.Rectangle(hDC, -1, -1, rc.Right - rc.Left, rc.Bottom - rc.Top);

                SSWindowsFunctions.SelectObject(hDC, hPrevPen);
                SSWindowsFunctions.SelectObject(hDC, hPrevBrush);
                
                SSWindowsFunctions.ReleaseDC(hWnd, hDC);
            }
            
        }

        private static void RefreshWindow(IntPtr hWnd)
        {
            SSWindowsFunctions.InvalidateRect(hWnd, IntPtr.Zero, true);
            SSWindowsFunctions.UpdateWindow(hWnd);
            SSWindowsFunctions.RedrawWindow(hWnd, IntPtr.Zero, IntPtr.Zero,
                  SSWindowsFunctions.RedrawWindowFlags.Frame 
                | SSWindowsFunctions.RedrawWindowFlags.Invalidate 
                | SSWindowsFunctions.RedrawWindowFlags.UpdateNow 
                | SSWindowsFunctions.RedrawWindowFlags.AllChildren);
        }

        #endregion


    }
}