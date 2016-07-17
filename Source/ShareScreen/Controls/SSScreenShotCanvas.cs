using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SS.ShareScreen.Extensions;
using Size = System.Drawing.Size;

namespace SS.ShareScreen.Controls
{
    public class SSScreenShotCanvas : Canvas
    {


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