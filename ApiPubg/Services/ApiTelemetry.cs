using ApiPubg.Models.SquadModel;
using ApiPubg.Models.TelemetriaModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace ApiPubg.Services
{
    public partial class ApiTelemetry : ObservableObject
    {
        private readonly HttpClient _httpClient;

        public ApiTelemetry()
        {
            var handler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };

            _httpClient = new HttpClient(handler);
        }

        public async Task<List<RootModel>> BuscarTelemetry(string telemetryPartida)
        {

            //using (_httpClient)
            //{
            //https://telemetry-cdn.pubg.com/bluehole-pubg/steam/2025/12/04/18/10/83cd646b-d13c-11f0-a791-6a353a7be83f-telemetry.json

                var response = await _httpClient.GetAsync(telemetryPartida);
                if (!response.IsSuccessStatusCode) return new();

                var json = await response.Content.ReadAsStringAsync();
                var telemetry = JsonConvert.DeserializeObject<List<RootModel>>(json);
                return telemetry! ;
            //}
        }
    }
}
