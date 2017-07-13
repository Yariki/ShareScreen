using System;
using System.Collections.Generic;
using ShareScreen.Core.Enums;
using ShareScreen.Core.Interfaces.Core;

namespace ShareScreen.Core.Core.MVVM
{
    public abstract class SSMenuItemViewModel : ISSMenuItemViewModel
    {
        private Action<object> _execute;
        private Func<object, bool> _canExecute; 


        public eSSMenuCommand MenuCommand => GetCommand();
        public string Icon => GetIcon();
        public bool IsParent { get; protected set; }
        public virtual IEnumerable<ISSMenuItemViewModel> SubItems { get; set; }
        public string UIName => GetUIName();
        public bool IsSeparator => GetIsSeparator();


        public SSMenuItemViewModel(Action<object> execute, Func<object,bool> canExecute )
        {
            _execute = execute;
            _canExecute = canExecute;
        }
        
        protected abstract eSSMenuCommand GetCommand();

        protected virtual string GetIcon() => "";

        protected virtual string GetUIName() => "";

        protected virtual bool GetIsSeparator() => false;

        public bool CanExecute(object parameter)
        {
            return _canExecute != null && _canExecute(this);
        }

        public void Execute(object parameter)
        {
            _execute?.Invoke(this);
        }

        public event EventHandler CanExecuteChanged;
    }
}