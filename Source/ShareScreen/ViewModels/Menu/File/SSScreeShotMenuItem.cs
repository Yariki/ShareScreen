using System;
using SS.ShareScreen.Core.MVVM;
using SS.ShareScreen.Enums;

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