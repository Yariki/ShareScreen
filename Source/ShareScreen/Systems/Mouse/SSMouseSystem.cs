using System;
using System.ComponentModel.Composition;
using System.Windows;
using SS.ShareScreen.Core.Systems;
using SS.ShareScreen.Interfaces.System;
using SS.ShareScreen.Windows;

namespace SS.ShareScreen.Systems.Mouse
{
    [Export(typeof(ISSMouseSystem))]
    public class SSMouseSystem : SSBaseHookSystem, ISSMouseSystem
    {

        private readonly SSWindowsFunctions.LowLevelMouseProc _proc = MouseHookProc;

        [ImportingConstructor]
        public SSMouseSystem()
            :base()
        {
        }
        
        public Tuple<Point, Point> GetSelectedArea()
        {
            return default(Tuple<Point, Point>);
        }

        public IntPtr GetSelectedWindow()
        {
            return default(IntPtr);
        }

        protected override Delegate GetCallback() => _proc;

        protected override int GetHookType() => SSWindowsFunctions.WH_MOUSE_LL;

        #region [private]

        private static IntPtr MouseHookProc(int nCode, int wParam, ref SSWindowsFunctions.MouseHookStruct lParam)
        {

            if (nCode >= 0 && (wParam == SSWindowsFunctions.WM_MOUSEMOVE))
            {
                System.Diagnostics.Debug.WriteLine($"X: {lParam.pt.x} - Y: {lParam.pt.y}");
            }
            return SSWindowsFunctions.CallNextHookEx(GetHookSystem().GetHookPtr(), nCode, wParam, SSWindowsFunctions.StructToPtr(lParam));
        }


        #endregion


    }
}