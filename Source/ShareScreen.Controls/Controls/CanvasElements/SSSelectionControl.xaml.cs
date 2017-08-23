using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ShareScreen.Controls.Controls.Adorners;
using ShareScreen.Controls.Controls.Core;
using ShareScreen.Controls.Controls.Thumbs;
using ShareScreen.Controls.EventArguments;
using ShareScreen.Core.Extensions;

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

        #region [events]

        public event EventHandler<SSSelectionPositionChangedArgs> PositionChanged;

        public event EventHandler<SSSelectionSizeChangedArgs> SelectionSizeChanged; 

        #endregion

        #region [dependency properties]

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
        #endregion

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var moveThumb = GetTemplateChild("PART_MoveThumb") as SSMoveThumb;
            if (moveThumb.IsNotNull())
            {
                moveThumb.DragDelta += MoveThumbOnDragDelta;
            }
            var decorator = GetTemplateChild("ItemDecorator") as SSDesignerItemDecorator;
            if (decorator.IsNotNull())
            {
                decorator.SelectionSizeChanged += ResizeAdornerOnSizeChanged;
            }
        }

        private void ResizeAdornerOnSizeChanged(object sender, SSSelectionSizeChangedArgs sizeChangedEventArgs)
        {
            var temp = SelectionSizeChanged;
            if (temp.IsNotNull())
            {
                temp(this, sizeChangedEventArgs);
            }
        }

        private void MoveThumbOnDragDelta(object sender, DragDeltaEventArgs dragDeltaEventArgs)
        {
            var temp = PositionChanged;
            if (temp.IsNotNull())
            {
                temp(this,new SSSelectionPositionChangedArgs(dragDeltaEventArgs.VerticalChange,dragDeltaEventArgs.HorizontalChange));
            }
        }
    }
}
