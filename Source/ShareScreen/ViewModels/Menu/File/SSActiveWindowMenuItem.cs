using System;
using ShareScreen.Core.Core.MVVM;
using ShareScreen.Core.Enums;

namespace SS.ShareScreen.ViewModels.Menu.File
{
    public class SSActiveWindowMenuItem : SSMenuItemViewModel
    {
        public SSActiveWindowMenuItem(Action<object> execute, Func<object, bool> canExecute) : base(execute, canExecute)
        {
        }

        protected override eSSMenuCommand GetCommand()
        {
            return eSSMenuCommand.MakeActiveWindow;
        }

        protected override string GetUIName()
        {
            return "Active Window";
        }
    }
}