using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ApiPubg.Converters
{
    public class PosicaoConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null) return null;

           int valor = (int)value;
            string text;

            if (valor == 1)
            {
                text = "Vencedor";
            }else
                if(valor == 2)
            {
                text = "Segundo";
            } else
                if(valor == 3)
            {
                text = "terceiro";
            } else { text = valor.ToString() + "º"; }

                return text;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
