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
using ShareScreen.Controls.Controls;
using ShareScreen.Core.Core.Payload;
using ShareScreen.Core.InteractionProviders;
using ShareScreen.Core.Interfaces.Controls;
using ShareScreen.Core.Interfaces.InteractionManager;

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
        private ISSInteractionManager _interactionManager;

        [ImportingConstructor]
        public SSSelectionWindow([Import(typeof(ISSInteractionManager))]ISSInteractionManager InteractionManager)
        {
            InitializeComponent();
            _canvas = new SSSelectionCanvas(InteractionManager);
            RootGrid.Children.Add(_canvas);
            this.KeyDown += OnKeyDown;
            _interactionManager = InteractionManager;
            Topmost = true;
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
                    _interactionManager.GetCommand<SSSelectionRegionProvider>().Publish(new SSPayload<Tuple<bool, Point, Point>>(new Tuple<bool, Point, Point>(false,default(Point),default(Point))));
                    Close();
                    break;
            }

        }
    }
}
