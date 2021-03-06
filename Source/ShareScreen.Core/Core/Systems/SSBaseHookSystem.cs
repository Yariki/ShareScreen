﻿using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using ShareScreen.Core.Interfaces.InteractionManager;
using ShareScreen.Core.Interfaces.System;
using ShareScreen.Core.Windows;

namespace ShareScreen.Core.Core.Systems
{
    public abstract class SSBaseHookSystem : ISSSystem
    {
        protected static IntPtr _hhook = IntPtr.Zero;

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
            //IntPtr ptrUser = SSWindowsFunctions.LoadLibrary("User32");
            using (ProcessModule module = Process.GetCurrentProcess().MainModule)
            {
                _hhook = SSWindowsFunctions.SetWindowsHookEx(GetHookType(), GetCallback(), SSWindowsFunctions.GetModuleHandle(module.ModuleName), 0);
            }
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