using System.Windows;
using System.Windows.Controls;

namespace ShareScreen.Controls.Controls
{
    public class SSResizeChrome : Control
    {
        static SSResizeChrome()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(SSResizeChrome), (PropertyMetadata)new FrameworkPropertyMetadata((object)typeof(SSResizeChrome)));
        }
    }
}