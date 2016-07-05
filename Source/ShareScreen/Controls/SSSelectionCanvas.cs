using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using SS.ShareScreen.Core.InteractionManager;
using SS.ShareScreen.Core.Payload;
using SS.ShareScreen.InteractionProviders;
using SS.ShareScreen.Interfaces.InteractionManager;

namespace SS.ShareScreen.Controls
{
    public class SSSelectionCanvas : Canvas
    {

        private Point? _start;
        private Point? _end;
        private ISSInteractionManager _interactionManager;

        public SSSelectionCanvas(ISSInteractionManager InteractionManager)
        {
            _interactionManager = InteractionManager;
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            if (_start.HasValue && _end.HasValue)
            {
                dc.DrawRectangle(Brushes.Gray, new Pen(Brushes.Black,1),new Rect(_start.Value,_end.Value));
            }
        }

        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonDown(e);
            _start = e.GetPosition(this);
        }

        protected override void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonUp(e);
            _interactionManager.GetCommand<SSSelectionRegionProvider>().Publish(new SSPayload<Tuple<bool,Point, Point>>(new Tuple<bool,Point, Point>(true,_start.Value,_end.Value)));
        }


        protected override void OnPreviewMouseMove(MouseEventArgs e)
        {
            base.OnPreviewMouseMove(e);
            _end = e.GetPosition(this);
            if (_start.HasValue && _end.HasValue)
            {
                InvalidateVisual();
            }            
        }

    }
}