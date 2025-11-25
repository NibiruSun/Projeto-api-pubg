using ApiPubg.Models.SquadModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ApiPubg.Converters
{
    public class NomeJogadorColorConverter : IValueConverter
    {

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
       {
            //if (parameter == null) return null;

            string? nomeJogadorBaseText = NomeJogador.NomeJogadorStatic;

            //if(parameter is Entry entry)
            //{
            //    nomeJogadorBaseText = entry.Text;
            //}
            //else if (parameter is string str)
            //{
            //    nomeJogadorBaseText = str;
            //}

            if (nomeJogadorBaseText == null || value == null) return null;

            var nomeJogador = value.ToString();

            return nomeJogador == nomeJogadorBaseText ? Color.FromArgb("#D6C1A6") : Color.FromArgb("#FFFFFF") ;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}