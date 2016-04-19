using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using SS.ShareScreen.Interfaces.Core;
using SS.ShareScreen.Interfaces.Main;

namespace SS.ShareScreen.Views.Main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [Export(typeof(ISSMainView))]
    public partial class MainWindow : MetroWindow, ISSMainView
    {
        
        private Dictionary<ISSView,BaseMetroDialog> 
            _dialogs = new Dictionary<ISSView, BaseMetroDialog>(); 

        public MainWindow()
        {
            InitializeComponent();
        }

        public ISSViewModel Model
        {
            get { return DataContext as ISSViewModel;}
            set { DataContext = value; }
        }
        public void ShowDialog(ISSView view)
        {
            Contract.Requires(view != null);
            var dialog = new MahApps.Metro.Controls.Dialogs.CustomDialog();
            dialog.DialogTop = view;
            _dialogs.Add(view,dialog);
            this.ShowMetroDialogAsync(dialog);
        }

        public void HideDialog(ISSView view)
        {
            Contract.Requires(view != null);
            Contract.Assert(!_dialogs.ContainsKey(view));
            var dialog = _dialogs[view];
            _dialogs.Remove(view);
            this.HideMetroDialogAsync(dialog);
        }

        public void MaximazeMainWindow()
        {
            this.WindowState = WindowState.Maximized;
        }

        public void MinimizeMainWindow()
        {
            this.WindowState = WindowState.Minimized;
        }

        public void NormalizeMainWindow()
        {
            this.WindowState = WindowState.Normal;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Application.Current.Shutdown(0);
        }
    }
}
