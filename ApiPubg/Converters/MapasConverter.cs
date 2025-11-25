using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ApiPubg.Converters
{
    class MapasConverter : IValueConverter
    {
        private static readonly Dictionary<string, string> MapNames = new()
    {
        { "Desert_Main", "Miramar" },
        { "Baltic_Main", "Erangel" },
        { "Savage_Main", "Sanhok" },
        { "DihorOtok_Main", "Vikendi" },
        { "Karakin", "Karakin" },
        { "Chimera_Main", "Paramo" },
        { "Heaven_Main", "Haven" },
        { "Tiger_Main", "Taego" },
        { "Kiki_Main", "Deston" },
        {"Neon_Main", "Neon" }
    };

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is not string mapCode)
                return null;

            return MapNames.TryGetValue(mapCode, out var mapName) ? mapName : mapCode;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
