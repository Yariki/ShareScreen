using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using ShareScreen.Controls.Controls.Adorners;
using ShareScreen.Core.Extensions;
using Point =  System.Windows.Point;

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
            "ScreenShot", typeof(ImageSource), typeof(SSScreenShotCanvas), new PropertyMetadata(default(Bitmap),ScreenShotChanged));

        private static void ScreenShotChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            ((SSScreenShotCanvas)dependencyObject).OnScreenShotChanged(dependencyPropertyChangedEventArgs.NewValue as ImageSource);
        }
        

        public ImageSource ScreenShot
        {
            get { return (ImageSource) GetValue(ScreenShotProperty); }
            set { SetValue(ScreenShotProperty, value); }
        }


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

        private void OnScreenShotChanged(ImageSource newValue)
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

        #endregion



    }
}