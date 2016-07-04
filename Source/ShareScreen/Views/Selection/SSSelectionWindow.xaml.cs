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
using System.Windows.Shapes;
using SS.ShareScreen.Controls;
using SS.ShareScreen.Interfaces.Controls;
using SS.ShareScreen.Interfaces.InteractionManager;

namespace SS.ShareScreen.Views.Selection
{
    /// <summary>
    /// Interaction logic for SSSelectionWindow.xaml
    /// </summary>
    [Export(typeof(ISSSelectionWindow))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class SSSelectionWindow : Window, ISSSelectionWindow
    {
        private SSSelectionCanvas _canvas;

        [ImportingConstructor]
        public SSSelectionWindow([Import(typeof(ISSInteractionManager))]ISSInteractionManager InteractionManager)
        {
            InitializeComponent();
            _canvas = new SSSelectionCanvas(InteractionManager);
            RootGrid.Children.Add(_canvas);
            this.KeyDown += OnKeyDown;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _canvas.Focus();
            _canvas.Background = Brushes.LightGray;
        }


        private void OnKeyDown(object sender, KeyEventArgs keyEventArgs)
        {
            switch (keyEventArgs.Key)
            {
                case Key.Escape:
                    this.KeyDown -= OnKeyDown;
                    Close();
                    break;
            }

        }
    }
}
