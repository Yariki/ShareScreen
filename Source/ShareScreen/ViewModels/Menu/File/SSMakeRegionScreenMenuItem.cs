using System;
using SS.ShareScreen.Core.MVVM;
using SS.ShareScreen.Enums;

namespace SS.ShareScreen.ViewModels.Menu.File
{
    public class SSMakeRegionScreenMenuItem : SSMenuItemViewModel
    {
        public SSMakeRegionScreenMenuItem(Action<object> execute, Func<object, bool> canExecute) : base(execute, canExecute)
        {
        }

        protected override eSSMenuCommand GetCommand()
        {
            return eSSMenuCommand.MakeRegionScreen;
        }

        protected override string GetUIName()
        {
            return "Region";
        }
    }
}