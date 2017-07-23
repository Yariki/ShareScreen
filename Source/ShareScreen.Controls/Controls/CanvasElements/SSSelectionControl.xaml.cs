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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ShareScreen.Controls.Controls.Core;

namespace ShareScreen.Controls.Controls.CanvasElements
{
    /// <summary>
    /// Interaction logic for SSSelectionControl.xaml
    /// </summary>
    public partial class SSSelectionControl : SSBaseCanvasElement
    {
        public SSSelectionControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty StrokeProperty = DependencyProperty.Register(
            "Stroke", typeof(Color), typeof(SSSelectionControl), new PropertyMetadata(Colors.Black));

        public Color Stroke
        {
            get { return (Color)GetValue(StrokeProperty); }
            set { SetValue(StrokeProperty, value); }
        }


        public static readonly DependencyProperty FillProperty = DependencyProperty.Register(
            "Fill", typeof(Color), typeof(SSSelectionControl), new PropertyMetadata(Colors.Transparent));

        public Color Fill
        {
            get { return (Color)GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }


        public static readonly DependencyProperty StrokeThiknessProperty = DependencyProperty.Register(
            "StrokeThikness", typeof(int), typeof(SSSelectionControl), new PropertyMetadata(1));

        public int StrokeThikness
        {
            get { return (int)GetValue(StrokeThiknessProperty); }
            set { SetValue(StrokeThiknessProperty, value); }
        }

    }
}
