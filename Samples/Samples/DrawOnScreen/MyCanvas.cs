using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DrawOnScreen
{
    public class MyCanvas : Canvas
    {

        private Point? _startPoint;
        private Point? _endPoint;
        public MyCanvas()
        {
        }

        protected override void OnPreviewMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            _endPoint = e.GetPosition(this);
            System.Diagnostics.Debug.WriteLine("OnMouseMove");
            if (_startPoint.HasValue && _endPoint.HasValue)
            {
                this.InvalidateVisual();
            }
        }

        protected override void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);
            _startPoint = null;
            _endPoint = null;
            System.Diagnostics.Debug.WriteLine("OnMouseLeftButtonUp");
        }

        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            System.Diagnostics.Debug.WriteLine("OnMouseLeftButtonDown");
            _startPoint = e.GetPosition(this);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            drawingContext.DrawRectangle(Brushes.Red, new Pen(Brushes.Blue, 2), new Rect(new Point(10, 10), new Size(100, 100)));
            if (_startPoint.HasValue && _endPoint.HasValue)
            {
                drawingContext.DrawRectangle(Brushes.Aqua, new Pen(Brushes.Blue, 1), new Rect(_startPoint.Value, _endPoint.Value));
                System.Diagnostics.Debug.WriteLine("Render");
                System.Diagnostics.Debug.WriteLine(_startPoint.Value.ToString() + " : " + _endPoint.Value.ToString());
            }
            System.Diagnostics.Debug.WriteLine("Render");

        }
    }
}