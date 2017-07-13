using System;
using ShareScreen.Core.Core.MVVM;
using ShareScreen.Core.Enums;

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