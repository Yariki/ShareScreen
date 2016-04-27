using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using SS.ShareScreen.Interfaces.Core;
using SS.ShareScreen.Interfaces.Main;

namespace SS.ShareScreen.Views.Main
{
    /// <summary>
    /// Interaction logic for SSScreenShotView.xaml
    /// </summary>
    [Export(typeof(ISSScreenShotView))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class SSScreenShotView : UserControl, ISSScreenShotView
    {
        public SSScreenShotView()
        {
            InitializeComponent();
        }

        public ISSViewModel Model
        {
            get { return DataContext as ISSViewModel; }
            set { DataContext = value; }
        }
    }
}
