using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using ShareScreen.Controls.Controls.Core;

namespace ShareScreen.Controls.Controls.Thumbs
{
    public class SSMoveThumb : Thumb
    {
        private bool _fromKey;

        public SSMoveThumb()
        {
            this.DragDelta += new DragDeltaEventHandler(this.MoveThumb_DragDelta);
        }

        private void MoveThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            var element = this.DataContext as SSBaseCanvasElement;
            var parent = VisualTreeHelper.GetParent((DependencyObject)element) as Canvas;
            bool fromKey = this._fromKey;
            this._fromKey = false;
            if (parent == null || element == null || !element.IsSelected)
                return;
            if (Keyboard.Modifiers != ModifierKeys.Control)
                return;
            double right = (double)parent.GetValue(FrameworkElement.WidthProperty);
            double bottom = (double)parent.GetValue(FrameworkElement.HeightProperty);
            if (e.HorizontalChange <= 0.0 && e.VerticalChange <= 0.0)
            {
                double x1 = Canvas.GetLeft((UIElement)element) + e.HorizontalChange;
                double y1 = Canvas.GetTop((UIElement)element) + e.VerticalChange;
                Point point = new Point(x1, y1);
                double x2 = point.X;
                Canvas.SetLeft((UIElement)element, x2);
                double y2 = point.Y;
                Canvas.SetTop((UIElement)element, y2);
            }
            else if (e.HorizontalChange >= 0.0 && e.VerticalChange >= 0.0)
            {
                double num3 = this.DeltaRight(e.HorizontalChange, right);
                double num4 = this.DeltaBottom(e.VerticalChange, bottom);
                double x1 = Canvas.GetLeft((UIElement)element) + num3;
                double y1 = Canvas.GetTop((UIElement)element) + num4;
                Point point = new Point(x1, y1);
                double x2 = point.X;
                Canvas.SetLeft((UIElement)element, x2);
                double y2 = point.Y;
                Canvas.SetTop((UIElement)element, y2);
            }
            else if (e.HorizontalChange <= 0.0 && e.VerticalChange >= 0.0)
            {
                double num4 = e.VerticalChange >= bottom ? 0.0 : e.VerticalChange;
                double x1 = Canvas.GetLeft((UIElement)element) + e.HorizontalChange;
                double y1 = Canvas.GetTop((UIElement)element) + num4;
                Point point = new Point(x1, y1);
                double x2 = point.X;
                Canvas.SetLeft((UIElement)element, x2);
                double y2 = point.Y;
                Canvas.SetTop((UIElement)element, y2);
            }
            else if (e.HorizontalChange >= 0.0 && e.VerticalChange <= 0.0)
            {
                double num3 = e.HorizontalChange >= right ? 0.0 : e.HorizontalChange;
                    double x1 = Canvas.GetLeft((UIElement)element) + num3;
                    double y1 = Canvas.GetTop((UIElement)element) + e.VerticalChange;
                    Point point = new Point(x1, y1);
                    double x2 = point.X;
                    Canvas.SetLeft((UIElement)element, x2);
                    double y2 = point.Y;
                    Canvas.SetTop((UIElement)element, y2);
            }
        }

        private double DeltaRight(double horizontalChange, double right)
        {
            if (horizontalChange == right)
                return 0.0;
            if (horizontalChange > right)
                return right;
            return horizontalChange;
        }

        private double DeltaBottom(double verticalChange, double bottom)
        {
            if (verticalChange == bottom)
                return 0.0;
            if (verticalChange > bottom)
                return bottom;
            return verticalChange;
        }


    }
}