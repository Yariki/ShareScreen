using System.Windows;
using System.Windows.Controls;
using ShareScreen.Core.Core.MVVM;

namespace ShareScreen.Controls.Controls.Selectors
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