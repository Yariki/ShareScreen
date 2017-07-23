using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using ShareScreen.Controls.Controls.Core;

namespace ShareScreen.Controls.Controls.Thumbs
{
    public class SSResizeThumb : Thumb
    {
        public SSResizeThumb()
        {
            this.DragDelta += new DragDeltaEventHandler(this.ResizeThumb_DragDelta);
        }

        private void ResizeThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            var element = this.DataContext as SSBaseCanvasElement;
            var parent = VisualTreeHelper.GetParent((DependencyObject)element) as Canvas;
            if (element != null && parent != null && element.IsSelected)
            {
                double right = (double)parent.GetValue(FrameworkElement.WidthProperty);
                double bottom = (double)parent.GetValue(FrameworkElement.HeightProperty);
                double aspect = element.GetAspect(true);
                bool flag = false;
                switch (this.HorizontalAlignment)
                {
                    case HorizontalAlignment.Left:
                        double val2_1 = Math.Min(e.HorizontalChange, element.ActualWidth - element.MinWidth);
                        double num3 = val2_1;
                        if (aspect <= 0.0)
                        {
                            double left = Canvas.GetLeft((UIElement)element);
                            double length = Canvas.GetLeft((UIElement)element) + num3;
                            Canvas.SetLeft((UIElement)element, length);
                            this.CheckItemMinWidth(element, element.Width + (left - length));
                            break;
                        }
                        Canvas.SetLeft((UIElement)element, Canvas.GetLeft((UIElement)element) + num3);
                        this.CheckItemMinWidth(element, element.Width - num3);
                        if (aspect > 0.0)
                        {
                            this.CheckItemMinHeight(element, element.Width / aspect);
                            flag = true;
                            break;
                        }
                        break;
                    case HorizontalAlignment.Right:
                        double horizontalChange = Math.Min(-e.HorizontalChange, element.ActualWidth - element.MinWidth);
                        double num4 = this.DeltaRight(horizontalChange, right);
                        if (element.Width - num4 < 0.0)
                            num4 = 0.0;
                        if (aspect <= 0.0)
                        {
                            this.CheckItemMinWidth(element, (int)(element.Width - num4));
                            break;
                        }
                        this.CheckItemMinWidth(element, element.Width - num4);
                        if (aspect > 0.0)
                        {
                            this.CheckItemMinHeight(element, element.Width / aspect);
                            flag = true;
                            break;
                        }
                        break;
                }
                if (!flag)
                {
                    switch (this.VerticalAlignment)
                    {
                        case VerticalAlignment.Top:
                            double val2_2 = Math.Min(e.VerticalChange, element.ActualHeight - element.MinHeight);
                            double num5 = val2_2;
                            double top = Canvas.GetTop((UIElement)element);
                            double length1 = top + num5;
                            if (aspect <= 0.0)
                                length1 = (int)length1;
                            Canvas.SetTop((UIElement)element, length1);
                            this.CheckItemMinHeight(element, element.Height + (top - length1));
                            if (aspect > 0.0)
                            {
                                this.CheckItemMinWidth(element, element.Height * aspect);
                            }
                            break;
                        case VerticalAlignment.Bottom:
                            double verticalChange = Math.Min(-e.VerticalChange, element.ActualHeight - element.MinHeight);
                            double num6 = this.DeltaBottom(verticalChange, bottom);
                            if (element.Height - num6 < 0.0)
                                num6 = 0.0;
                            if (aspect <= 0.0)
                            {
                                this.CheckItemMinHeight(element, element.Height);
                                
                            }
                            this.CheckItemMinHeight(element, element.Height - num6);
                            if (aspect > 0.0)
                            {
                                this.CheckItemMinWidth(element, element.Height * aspect);
                                
                            }
                            break;
                    }
                }
            }
            e.Handled = true;
        }

        private double DeltaRight(double horizontalChange, double right)
        {
            if ( horizontalChange == right)
                return 0.0;
            if ( horizontalChange > right)
                return right;
            return horizontalChange;
        }

        private double DeltaBottom(double verticalChange, double bottom)
        {
            if (verticalChange == bottom)
                return 0.0;
            if ( verticalChange > bottom)
                return bottom;
            return verticalChange;
        }
        
        private void CheckItemMinWidth(SSBaseCanvasElement item, double width)
        {
            if (width > item.MinWidth)
                item.Width = width;
            else
                item.Width = item.MinWidth;
        }

        private void CheckItemMinHeight(SSBaseCanvasElement item, double height)
        {
            if (height > item.MinHeight)
                item.Height = height;
            else
                item.Height = item.MinHeight;
        }
    }
}