using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics.Contracts;
using SS.ShareScreen.Interfaces.InteractionManager;
using SS.ShareScreen.Interfaces.System;
using SS.ShareScreen.Windows;

namespace SS.ShareScreen.Core.Systems
{
    public abstract class SSBaseHookSystem : ISSSystem
    {
        private static IntPtr _hhook = IntPtr.Zero;

        private static SSBaseHookSystem _this;


        protected SSBaseHookSystem()
        {
            _this = this;
        }

        [Import]
        public ISSInteractionManager InteractionManager { get; set; }

        [Import]
        protected CompositionContainer Container { get; set; }


        public virtual void StartSystem()
        {
            SetHook();
        }

        public virtual void StopSystem()
        {
            ReleaseHook();
        }

        private void SetHook()
        {
            Contract.Requires(GetHookType() > 0);
            Contract.Requires(GetCallback() != null);
            IntPtr ptrUser = SSWindowsFunctions.LoadLibrary("User32");
            _hhook = SSWindowsFunctions.SetWindowsHookEx(GetHookType(),GetCallback(),ptrUser,0);
        }

        private void ReleaseHook()
        {
            Contract.Requires(_hhook != IntPtr.Zero);
            SSWindowsFunctions.UnhookWindowsHookEx(_hhook);
        }

        public IntPtr GetHookPtr() => _hhook;

        protected abstract Delegate GetCallback();

        protected abstract int GetHookType();

        protected static SSBaseHookSystem GetHookSystem() => _this;

    }
}