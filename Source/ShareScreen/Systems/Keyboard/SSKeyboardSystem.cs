using System;
using System.ComponentModel.Composition;
using SS.ShareScreen.Interfaces.System;
using SS.ShareScreen.Windows;

namespace SS.ShareScreen.Systems.Keyboard
{
    [Export(typeof(ISSKeyboardSystem))]
    public class SSKeyboardSystem : ISSKeyboardSystem
    {
        const int WH_KEYBOARD_LL = 13; 
        const int WM_KEYDOWN = 0x100; 

        private SSWindowsFunctions.LowLevelKeyboardProc _proc = KeyboardHookProc;

        private static IntPtr hhook = IntPtr.Zero;

        public void StartSystem()
        {
            SetHook();
        }

        public void StopSystem()
        {
            ReleaseHook();
        }
        
        private void SetHook()
        {
            IntPtr ptrUser32 = SSWindowsFunctions.LoadLibrary("User32");
            hhook = SSWindowsFunctions.SetWindowsHookEx(WH_KEYBOARD_LL, _proc,ptrUser32,0);
        }

        private void ReleaseHook()
        {
            SSWindowsFunctions.UnhookWindowsHookEx(hhook);
        }

        private static IntPtr KeyboardHookProc(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code >= 0 && wParam == (IntPtr) WM_KEYDOWN)
            {
                return (IntPtr) 1;
            }
            return SSWindowsFunctions.CallNextHookEx(hhook, code, (int)wParam, lParam);
        }
    }
}