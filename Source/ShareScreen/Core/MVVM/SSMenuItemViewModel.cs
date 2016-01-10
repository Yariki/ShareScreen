using System;
using System.Collections.Generic;
using System.Windows.Input;
using SS.ShareScreen.Core.Command;
using SS.ShareScreen.Enums;
using SS.ShareScreen.Interfaces.Core;

namespace SS.ShareScreen.Core.MVVM
{
    public abstract class SSMenuItemViewModel : ISSMenuItemViewModel
    {
        public eSSMenuCommand MenuCommand => GetCommand();
        public string Icon => GetIcon();
        public bool IsParent { get; protected set; }
        public virtual IEnumerable<ISSMenuItemViewModel> SubItems { get; set; }
        public string UIName => GetUIName();

        public SSMenuItemViewModel(Action<object> execute, Func<object,bool> canExecute )
        {
            Command = new SSCommand(execute, canExecute);
        }

        public ICommand Command { get; private set; }
        
        protected abstract eSSMenuCommand GetCommand();

        protected virtual string GetIcon() => "";

        protected virtual string GetUIName() => "";

    }
}