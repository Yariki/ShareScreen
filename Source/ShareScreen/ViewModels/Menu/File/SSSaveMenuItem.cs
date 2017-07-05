using System;
using SS.ShareScreen.Core.MVVM;
using SS.ShareScreen.Enums;

namespace SS.ShareScreen.ViewModels.Menu.File
{
    public class SSSaveMenuItem : SSMenuItemViewModel
    {
        public SSSaveMenuItem(Action<object> execute, Func<object, bool> canExecute) : base(execute, canExecute)
        {
        }

        protected override eSSMenuCommand GetCommand() => eSSMenuCommand.Save;

        protected override string GetUIName() => "Save";
        
    }
}