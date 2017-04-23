using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using SS.ShareScreen.Core.InteractionManager;
using SS.ShareScreen.Core.Payload;
using SS.ShareScreen.Core.Systems;
using SS.ShareScreen.Enums;
using SS.ShareScreen.Extensions;
using SS.ShareScreen.InteractionProviders;
using SS.ShareScreen.Interfaces.Controls;
using SS.ShareScreen.Interfaces.InteractionManager;
using SS.ShareScreen.Interfaces.System;
using SS.ShareScreen.Views.Hightlight;
using SS.ShareScreen.Windows;
using Color = System.Drawing.Color;
using Pen = System.Drawing.Pen;
using WindowsApplication = System.Windows;


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
        private ISSSelectionWindow _selectionWindow;
        private ISSSubscribeToken _selctionAreaToken;
        private WindowsApplication.Point pt1;
        private WindowsApplication.Point pt2;
        private Thread _mouseHookThread;
        private uint _mouseHookThreadId;
        private static Dispatcher _currentDispatcher;
        private static SSHighlightWindow _highlightWindow;



        [ImportingConstructor]
        public SSMouseSystem()
            : base()
        {
            _currentDispatcher = Dispatcher.CurrentDispatcher;
            _highlightWindow = new SSHighlightWindow();
        }


        public override void StartSystem()
        {
            _mouseHookThread = new Thread(() =>
            {
                _mouseHookThreadId = (uint)SSWindowsFunctions.GetCurrentThreadId();
                using (ProcessModule module = Process.GetCurrentProcess().MainModule)
                {
                    _hhook = SSWindowsFunctions.SetWindowsHookEx(GetHookType(), GetCallback(), SSWindowsFunctions.GetModuleHandle(module.ModuleName), 0);
                }
                uint msg;
                SSWindowsFunctions.GetMessage(out msg, IntPtr.Zero, 0, 0);
            });
            _mouseHookThread.IsBackground = true;
            _mouseHookThread.Start();
            _selctionAreaToken = InteractionManager.GetCommand<SSSelectionRegionProvider>().Subscribe(OnSelectionArea);
        }
        

        public override void StopSystem()
        {
            SSWindowsFunctions.UnhookWindowsHookEx(_hhook);
            if (_mouseHookThread != null)
            {
                SSWindowsFunctions.PostThreadMessage(_mouseHookThreadId, 0, UIntPtr.Zero, IntPtr.Zero);
                _mouseHookThread = null;
                _mouseHookThreadId = 0;
            }
            InteractionManager.GetCommand<SSSelectionRegionProvider>().Unsubscribe(_selctionAreaToken);
        }

        public Tuple<WindowsApplication.Point, WindowsApplication.Point> GetSelectedArea()
        {
            return new Tuple<WindowsApplication.Point, WindowsApplication.Point>(pt1,pt2);
        }

        public IntPtr GetSelectedWindow()
        {
            return _selectedWindow;
        }

        public void RunSelectingWindow(IntPtr mainHwnd)
        {
            _selectedWindow = IntPtr.Zero;
            _screenshotType = eScreenshotType.SelectedWindow;
        }

        public void RunSelectingArea()
        {
            _screenshotType = eScreenshotType.SelectedArea;
            _selectionWindow = Container.GetExportedValue<ISSSelectionWindow>();
            _selectionWindow?.Show();
        }

        public void ResetCurrentAction()
        {
            _screenshotType = eScreenshotType.None;
        }

        protected override Delegate GetCallback() => _proc;

        protected override int GetHookType() => SSWindowsFunctions.WH_MOUSE_LL;

        #region [private]

        private void OnSelectionArea(SSPayload<Tuple<bool, WindowsApplication.Point, WindowsApplication.Point>> ssPayload)
        {
            if (ssPayload.Value.Item1)
            {
                _selectionWindow.Close();
                pt1 = ssPayload.Value.Item2;
                pt2 = ssPayload.Value.Item3;
                InteractionManager.GetCommand<SSSelectionRegionFinished>().Publish(new SSPayload<bool>(true));
            }
            else
            {
                InteractionManager.GetCommand<SSSelectionRegionFinished>().Publish(new SSPayload<bool>(false));
            }
        }

        private static IntPtr MouseHookProc(int nCode, int wParam, ref SSWindowsFunctions.MouseHookStructLL lParam)
        {

            if (nCode >= 0)
            {
                switch (_screenshotType)
                {
                    case eScreenshotType.SelectedWindow:
                        
                        var pt = new SSWindowsFunctions.POINT() {x= lParam.pt.x, y = lParam.pt.y};
                        _selectedWindow = SSWindowsFunctions.WindowFromPoint(pt);
                        _currentDispatcher.BeginInvoke((Action) (() =>
                        {
                            if (_selectedWindow != IntPtr.Zero && _selectedWindow != _oldHwnd)
                            {
                                HighlightingCurrentWindow(_selectedWindow);
                                var textOFSelectedWindow = SSWindowsFunctions.GetText(_selectedWindow);
                                System.Diagnostics.Debug.WriteLine($"Selected window {textOFSelectedWindow}...");
                                _oldHwnd = _selectedWindow;
                            }
                            ProcessSelectingWindow(wParam);
                        }));
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
                _currentDispatcher.BeginInvoke((Action)(() =>
                {
                    ((SSBaseHookSystem)GetHookSystem()).InteractionManager.GetCommand<SSSelectedWindowProvider>()
                        .Publish(new SSPayload<bool>(true));
                }));
                
                _screenshotType = eScreenshotType.None;
                _oldHwnd = IntPtr.Zero;
               _highlightWindow.Hide();
            }
            else if (wParam == SSWindowsFunctions.WM_RBUTTONDOWN)
            {
                _currentDispatcher.BeginInvoke((Action) (() =>
                {
                    ((SSBaseHookSystem) GetHookSystem()).InteractionManager.GetCommand<SSSelectedWindowProvider>()
                        .Publish(new SSPayload<bool>(false));
                }));
                _screenshotType = eScreenshotType.None;
                _oldHwnd = IntPtr.Zero;
                _highlightWindow.Hide();
            }
            
        }

        private static void HighlightingCurrentWindow(IntPtr hWnd)
        {
            SSWindowsFunctions.RECT rc = new SSWindowsFunctions.RECT();
            SSWindowsFunctions.GetWindowRect(hWnd, out rc);
            _highlightWindow.Left = rc.Left;
            _highlightWindow.Top = rc.Top;
            _highlightWindow.Width = rc.Right - rc.Left;
            _highlightWindow.Height = rc.Bottom - rc.Top;
            Debug.WriteLine($"Selected rec = {rc}");
            Debug.WriteLine($"Left:{_highlightWindow.Left} Top:{_highlightWindow.Top} Width:{_highlightWindow.Width} Height:{_highlightWindow.Height}");
            _highlightWindow.Show();
        }

        
        #endregion


    }
}