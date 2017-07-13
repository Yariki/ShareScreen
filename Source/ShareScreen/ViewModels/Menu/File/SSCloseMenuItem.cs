using System;
using ShareScreen.Core.Core.MVVM;
using ShareScreen.Core.Enums;

namespace SS.ShareScreen.ViewModels.Menu.File
{
    public class SSCloseMenuItem : SSMenuItemViewModel
    {
        public SSCloseMenuItem(Action<object> execute, Func<object, bool> canExecute) : base(execute, canExecute)
        {
        }

        protected override eSSMenuCommand GetCommand()
        {
            return eSSMenuCommand.Close;
        }

        protected override string GetUIName()
        {
            return "Exit";
        }
    }
}