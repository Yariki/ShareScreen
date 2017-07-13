using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ShareScreen.Core.Windows;

namespace SS.ShareScreen.Views.Hightlight
{
    /// <summary>
    /// Interaction logic for SSHighlightWindow.xaml
    /// </summary>
    public partial class SSHighlightWindow : Window
    {
        public SSHighlightWindow()
        {
            InitializeComponent();
        }
        
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            var hwnd = new WindowInteropHelper(this).Handle;
            SSWindowsFunctions.SetWindowExTransparent(hwnd);
        }
    }
}
