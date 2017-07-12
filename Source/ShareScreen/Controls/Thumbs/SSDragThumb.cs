using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace SS.ShareScreen.Controls.Thumbs
{
    public class SSDragThumb : Thumb
    {
        public SSDragThumb()
        {
            this.DragDelta += new DragDeltaEventHandler(this.DragThumb_DragDelta);
        }

        private void DragThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            var dataContext = this.DataContext as FrameworkElement;
            Canvas parent = VisualTreeHelper.GetParent((DependencyObject)dataContext) as Canvas;
            if (dataContext == null || parent == null)
                return;
            double maxValue1 = double.MaxValue;
            double maxValue2 = double.MaxValue;
            double left = Canvas.GetLeft((UIElement)dataContext);
            double top = Canvas.GetTop((UIElement)dataContext);
            double num1 = double.IsNaN(left) ? 0.0 : Math.Min(left, maxValue1);
            double num2 = double.IsNaN(top) ? 0.0 : Math.Min(top, maxValue2);
            double num3 = Math.Max(-num1, e.HorizontalChange);
            double num4 = Math.Max(-num2, e.VerticalChange);
            double d1 = Canvas.GetLeft((UIElement)dataContext);
            double d2 = Canvas.GetTop((UIElement)dataContext);
            if (double.IsNaN(d1))
                d1 = 0.0;
            if (double.IsNaN(d2))
                d2 = 0.0;
            Canvas.SetLeft((UIElement)dataContext, d1 + num3);
            Canvas.SetTop((UIElement)dataContext, d2 + num4);
            parent.InvalidateMeasure();
            e.Handled = true;
        }
    }
}