using System;
using SS.ShareScreen.Core.MVVM;
using SS.ShareScreen.Enums;

namespace SS.ShareScreen.ViewModels.Menu.File
{
    public class SSMakeAllScreenMenuItem : SSMenuItemViewModel
    {
        public SSMakeAllScreenMenuItem(Action<object> execute, Func<object, bool> canExecute) : base(execute, canExecute)
        {
        }

        protected override eSSMenuCommand GetCommand()
        {
            return eSSMenuCommand.MakeAllScreen;
        }

        protected override string GetUIName()
        {
            return "All Screen";
        }
    }
}