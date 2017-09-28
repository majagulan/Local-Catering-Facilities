using System;
using System.Windows.Data;
using System.Globalization;

namespace HCI_Lokali
{
    public class ConverterStringToInt : IValueConverter
    {
        string convString;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is int))
                return Binding.DoNothing;

            var stringValue = convString ?? value.ToString();
            convString = null;

            return stringValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string)) return Binding.DoNothing;

            double result;
            if (double.TryParse((string)value, out result))
            {
                convString = (string)value;
                return result;
            }

            return Binding.DoNothing;
        }
    }
}
