using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ShareScreen.Controls.Controls.Converters
{
    [ValueConversion(typeof(Brush), typeof(Color))]
    public class SSBrushColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (object)new SolidColorBrush((Color)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SolidColorBrush solidColorBrush = (SolidColorBrush)value;
            if (solidColorBrush != null)
                return (object)solidColorBrush.Color;
            return (object)Colors.Black;
        }
    }
}