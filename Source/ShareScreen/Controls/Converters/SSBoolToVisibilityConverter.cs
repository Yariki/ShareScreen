using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SS.ShareScreen.Controls.Converters
{
    public class SSBoolToVisibilityConverter : IValueConverter
    {
        public bool IsNegative { get; set; }


        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return IsNegative
                ? !((bool) value)
                    ? Visibility.Visible
                    : Visibility.Collapsed
                : ((bool) value)
                    ? Visibility.Visible
                    : Visibility.Collapsed;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return false;
        }
    }
}