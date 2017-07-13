using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using ShareScreen.Controls.Controls.Core;

namespace ShareScreen.Controls.Controls.Adorners
{
    public class SSResizeAdorner : Adorner
    {
        private VisualCollection visuals;
        private SSResizeChrome chrome;

        protected override int VisualChildrenCount
        {
            get
            {
                return this.visuals.Count;
            }
        }

        public SSResizeAdorner(ContentControl designerItem)
            : base((UIElement) designerItem)
        {
            this.SnapsToDevicePixels = true;
            this.chrome = new SSResizeChrome();
            this.chrome.DataContext = (object)designerItem;
            this.visuals = new VisualCollection((Visual)this);
            this.visuals.Add((Visual)this.chrome);
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            this.chrome.Arrange(new Rect(arrangeBounds));
            return arrangeBounds;
        }
        
        protected override Visual GetVisualChild(int index)
        {
            return this.visuals[index];
        }

    }
}