using System;
using ShareScreen.Core.Core.MVVM;
using ShareScreen.Core.Enums;

namespace SS.ShareScreen.ViewModels.Menu.File
{
    public class SSScreeShotMenuItem : SSMenuItemViewModel
    {
        public SSScreeShotMenuItem(Action<object> execute, Func<object, bool> canExecute) : base(execute, canExecute)
        {
        }

        protected override eSSMenuCommand GetCommand()
        {
            return eSSMenuCommand.ScreenShot;
        }

        protected override string GetUIName()
        {
            return "Screenshot";
        }
    }
}