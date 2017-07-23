using System;
using System.Globalization;
using System.Windows.Data;

namespace ShareScreen.Controls.Controls.Converters
{
    [ValueConversion(typeof(double),typeof(int))]
    public class SSDoubleToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            return (object)System.Convert.ToDouble(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (object)System.Convert.ToInt32(value);
        }
    }
}