using System;
using System.ComponentModel.Composition;
using System.Windows.Forms;
using SS.ShareScreen.Core.Systems;
using SS.ShareScreen.Interfaces.System;
using SS.ShareScreen.Windows;

namespace SS.ShareScreen.Systems.Keyboard
{
    [Export(typeof(ISSKeyboardSystem))]
    public class SSKeyboardSystem : SSBaseHookSystem, ISSKeyboardSystem
    {

        private static System.Windows.Forms.Keys LastKey;

        const int WH_KEYBOARD_LL = 13; 
        const int WM_KEYDOWN = 0x100; 

        private readonly SSWindowsFunctions.LowLevelKeyboardProc _proc = KeyboardHookProc;

        [ImportingConstructor]
        public SSKeyboardSystem()
            :base()
        {
        }
        
        protected override Delegate GetCallback() => _proc;

        protected override int GetHookType() => WH_KEYBOARD_LL;

        private static IntPtr KeyboardHookProc(int code, int wParam, ref SSWindowsFunctions.KeyboardHookStruct lParam)
        {
            if (code >= 0 && (wParam == SSWindowsFunctions.WM_KEYDOWN || wParam == SSWindowsFunctions.WM_SYSKEYDOWN))
            {
                var key = (System.Windows.Forms.Keys)Enum.Parse(typeof (System.Windows.Forms.Keys), lParam.vkCode.ToString());
                if (LastKey == Keys.LControlKey && key == Keys.C)
                {
                    System.Diagnostics.Debug.WriteLine($"Combination: {LastKey}+{key}");
                }
                LastKey = key;
            }
            return SSWindowsFunctions.CallNextHookEx( ((SSBaseHookSystem)GetHookSystem()).GetHookPtr() , code, (int)wParam, SSWindowsFunctions.StructToPtr(lParam));
        }
    }
}