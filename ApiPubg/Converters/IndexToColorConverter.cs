using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ApiPubg.Converters
{
    class IndexToColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null) return null;
            int index = (int)value;
            return index switch
            {
                1 => Colors.Yellow,
                2 => Colors.Orange,
                3 => Colors.Blue,
                4 => Colors.Green,
                _ => Colors.Gray // Valor padrão para cobrir todos os outros casos
            };
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
