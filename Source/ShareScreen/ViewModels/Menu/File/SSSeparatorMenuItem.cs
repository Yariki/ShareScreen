using System;
using ShareScreen.Core.Core.MVVM;
using ShareScreen.Core.Enums;

namespace SS.ShareScreen.ViewModels.Menu.File
{
    public class SSSeparatorMenuItem : SSMenuItemViewModel
    {
        public SSSeparatorMenuItem(Action<object> execute, Func<object, bool> canExecute) : base(execute, canExecute)
        {
        }

        protected override bool GetIsSeparator() => true;
        
        protected override eSSMenuCommand GetCommand()
        {
            return eSSMenuCommand.None;
        }
    }
}