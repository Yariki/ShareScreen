using System.Windows;
using System.Windows.Controls;
using SS.ShareScreen.Core.MVVM;

namespace SS.ShareScreen.Controls.Selectors
{
    public class SSMenuItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate MenuItem { get; set; }

        public DataTemplate SeparatorItem { get; set; }
        

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var menuItem = item as SSMenuItemViewModel;

            return menuItem.IsSeparator ? SeparatorItem : MenuItem;
        }
    }
}