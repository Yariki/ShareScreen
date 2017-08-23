using System;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ShareScreen.Controls.Controls.Adorners;
using ShareScreen.Controls.Controls.CanvasElements;
using ShareScreen.Controls.Controls.Core;
using ShareScreen.Controls.EventArguments;
using ShareScreen.Core.Extensions;
using Point =  System.Windows.Point;
using Size = System.Windows.Size;

namespace ShareScreen.Controls.Controls
{
    public class SSScreenShotCanvas : Canvas
    {

        private Point? _start;
        private bool _moving;

        public SSScreenShotCanvas()
        {
            LayoutTransform = new ScaleTransform();
        }

        #region [dependency properties]

        public static readonly DependencyProperty ScaleProperty = DependencyProperty.Register(
            "Scale", typeof(double), typeof(SSScreenShotCanvas), new PropertyMetadata(1.0,ScaleChanged));

        private static void ScaleChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            ((SSScreenShotCanvas)dependencyObject).OnScaleChanged((double)dependencyPropertyChangedEventArgs.NewValue);
        }

        public double Scale
        {
            get { return (double) GetValue(ScaleProperty); }
            set { SetValue(ScaleProperty, value); }
        }


        public static readonly DependencyProperty ScreenShotProperty = DependencyProperty.Register(
            "ScreenShot", typeof(BitmapSource), typeof(SSScreenShotCanvas), new PropertyMetadata(default(Bitmap),ScreenShotChanged));

        private static void ScreenShotChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            ((SSScreenShotCanvas)dependencyObject).OnScreenShotChanged(dependencyPropertyChangedEventArgs.NewValue as BitmapSource);
        }
        

        public BitmapSource ScreenShot
        {
            get { return (BitmapSource) GetValue(ScreenShotProperty); }
            set { SetValue(ScreenShotProperty, value); }
        }

        public static readonly DependencyProperty CurrentSelectionProperty = DependencyProperty.Register(
            "CurrentSelection", typeof(Rect), typeof(SSScreenShotCanvas), new PropertyMetadata(default(Rect)));

        public Rect CurrentSelection
        {
            get { return (Rect) GetValue(CurrentSelectionProperty); }
            set { SetValue(CurrentSelectionProperty, value); }
        }
        
        #endregion
        
        #region [public methods]


        public void AddSelection(Point start, double width, double height)
        {
            this.Children.OfType<SSSelectionControl>().ForEach(sel =>
            {
                sel.PositionChanged -= CtrlOnPositionChanged;
                sel.SelectionSizeChanged -= CtrlOnSelectionSizeChanged;
            });
            this.Children.Clear();

            var ctrl = new SSSelectionControl() {Width = width, Height = height};
            CurrentSelection = new Rect(start, new Size(width,height));
            Canvas.SetLeft(ctrl,start.X);
            Canvas.SetTop(ctrl,start.Y);
            Children.Add(ctrl);

            ctrl.PositionChanged += CtrlOnPositionChanged;
            ctrl.SelectionSizeChanged += CtrlOnSelectionSizeChanged;
        }

        

        #endregion
        
        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            if (ScreenShot.IsNotNull())
            {
                dc.DrawImage(ScreenShot,new Rect(new System.Windows.Point(0,0), new System.Windows.Size((int)this.Width,(int)this.Height)));
            }
            
        }


        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            _start = new Point?(e.GetPosition((IInputElement) this));
            DeselectAll();
            Focus();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.LeftButton != MouseButtonState.Pressed)
            {
                _start = new Point?();
            }
            if (_start.HasValue)
            {
                AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this);
                if (adornerLayer.IsNotNull())
                {
                    adornerLayer.Add(new SSSelectionAdorner(this,_start));
                }
            }
            if (e.LeftButton != MouseButtonState.Pressed || _moving)
            {
                return;
            }
            _moving = true;
        }

        protected override void OnPreviewMouseUp(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseUp(e);
            _moving = false;
        }


        #region [private]

        private void OnScreenShotChanged(BitmapSource newValue)
        {
            if (newValue.IsNotNull())
            {
                this.Width = newValue.Width;
                this.Height = newValue.Height;
            }
        }

        private void OnScaleChanged(double scale)
        {
            ((ScaleTransform) (LayoutTransform)).ScaleX = scale;
            ((ScaleTransform) (LayoutTransform)).ScaleY = scale;
        }

        private void DeselectAll()
        {
            if (!this.Children.OfType<SSBaseCanvasElement>().Any())
            {
                return;
            }
            this.Children.OfType<SSBaseCanvasElement>().ForEach(c => c.IsSelected = false);
            CurrentSelection = Rect.Empty;
        }

        private void CtrlOnPositionChanged(object sender, SSSelectionPositionChangedArgs ssSelectionPositionChangedArgs)
        {
            ProcessNewSelection(sender);
        }

        private void CtrlOnSelectionSizeChanged(object sender, SSSelectionSizeChangedArgs ssSelectionSizeChangedArgs)
        {
            ProcessNewSelection(sender);
        }

        private void ProcessNewSelection(object sender)
        {
            var left = Canvas.GetLeft((UIElement)sender);
            var top = Canvas.GetTop((UIElement)sender);
            var width = ((FrameworkElement)sender).ActualWidth;
            var height = ((FrameworkElement)sender).ActualHeight;
            CurrentSelection = new Rect(new Point(left, top), new Size(width, height));
        }


        #endregion



    }
}