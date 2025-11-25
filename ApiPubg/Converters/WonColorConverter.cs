using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ApiPubg.Converters
{
    public class WonColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null) return null;
            bool Won = (bool)value;

            return Won == true ? Color.FromArgb("#20B2AA") : Color.FromArgb("#525252");

        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
