using System;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Media;
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
    [PartCreationPolicy(CreationPolicy.Shared)]
    public sealed class SSMouseSystem : SSBaseHookSystem, ISSMouseSystem
    {

        private readonly SSWindowsFunctions.LowLevelMouseProc _proc = MouseHookProc;
        private static IntPtr _selectedWindow;
        private static IntPtr _oldHwnd;
        private static eScreenshotType _screenshotType = eScreenshotType.None;
        
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

        private static IntPtr MouseHookProc(int nCode, int wParam, ref SSWindowsFunctions.MouseHookStructLL lParam)
        {

            if (_oldHwnd != IntPtr.Zero)
            {
                SSWindowsFunctions.RefreshWindow(_oldHwnd);
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
            var hDC = SSWindowsFunctions.GetWindowDC(hWnd);

            if (hDC != IntPtr.Zero)
            {
                using (var graphics = Graphics.FromHdc(hDC))
                {
                    var pen = new Pen(Color.Red,3);
                    graphics.DrawRectangle(pen,0,0, rc.Right - rc.Left,rc.Bottom - rc.Top);
                }
                SSWindowsFunctions.ReleaseDC(hWnd, hDC);
            }
            
        }

        
        #endregion


    }
}