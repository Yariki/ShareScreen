using System;
using ShareScreen.Core.Core.MVVM;
using ShareScreen.Core.Enums;

namespace SS.ShareScreen.ViewModels.Menu.File
{
    public class SSSaveAsMenuItem : SSMenuItemViewModel
    {
        public SSSaveAsMenuItem(Action<object> execute, Func<object, bool> canExecute) : base(execute, canExecute)
        {
        }

        protected override eSSMenuCommand GetCommand() => eSSMenuCommand.SaveAs;

        protected override string GetUIName() => "Save As...";
        
    }
}