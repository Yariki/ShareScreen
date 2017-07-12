using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using SS.ShareScreen.Extensions;

namespace SS.ShareScreen.Controls.Adorners
{
    public class SSSelectionAdorner : Adorner
    {
        private Point? _start;
        private Point? _end;
        private Pen _pen;
        private Brush _brush;
        private SSScreenShotCanvas _canvas;

        public SSSelectionAdorner(SSScreenShotCanvas adornedElement) 
            : base(adornedElement)
        {
            _pen = new Pen((Brush)Brushes.DarkGray,1.0);
            _pen.DashStyle = DashStyles.Solid;
            _brush = (Brush) new SolidColorBrush(Colors.LightGray);
            _brush.Opacity = 0.25;
            _canvas = adornedElement;
        }


        public SSSelectionAdorner(SSScreenShotCanvas adornedElement, Point? start)
            : this(adornedElement)
        {
            _start = start;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (!this.IsMouseCaptured)
                {
                    this.CaptureMouse();
                    Mouse.OverrideCursor = Cursors.Cross;
                }
                _end = new Point?(e.GetPosition((IInputElement)this));
                InvalidateVisual();
            }
            else if (IsMouseCaptured)
            {
                ReleaseMouseCapture();
            }
            e.Handled = true;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            drawingContext.DrawRectangle((Brush)Brushes.Transparent,(Pen)null,new Rect(this.RenderSize));
            if (!_start.HasValue || !_end.HasValue)
            {
                return;
            }
            drawingContext.DrawRectangle(_brush,_pen,new Rect(_start.Value,_end.Value));
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            if (IsMouseCaptured)
            {
                ReleaseMouseCapture();
                Mouse.OverrideCursor = (Cursor) null;
            }
            AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(_canvas);
            if (adornerLayer.IsNotNull())
            {
                adornerLayer.Remove(this);
            }
            // TODO: add selection control to screenshot canvas.
            e.Handled = true;
        }
    }
}