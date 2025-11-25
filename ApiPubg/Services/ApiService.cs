
using ApiPubg.Models.SquadModel;
using ApiPubg.Models.SquadModel.MatchesModels;
using Newtonsoft.Json;
using System.Diagnostics;

namespace ApiPubg.Services
{
    public class ApiService
    {
        public static async Task<(HttpClient httpClient, CancellationTokenSource tokenSource)> CriarHttpClient()
        {
            var httpClient = new HttpClient();
            var tentativas = 1;

            while (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlertAsync("Sem conexão", $"Toque em conectar para tentar novamete \n\n Tentativas de comexão: [ {tentativas} ] ", "Conectar");
                tentativas++;
            }

            try
            {
                var cts = new CancellationTokenSource(TimeSpan.FromSeconds(20));
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Add("Accept", "application/vnd.api+json");
                httpClient.DefaultRequestHeaders.Add("Authorization", "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJqdGkiOiJkMjUyNmVkMC04NWFmLTAxM2UtZmFkOS0yNmRiNzVmMTk4MzciLCJpc3MiOiJnYW1lbG9ja2VyIiwiaWF0IjoxNzU5ODQzODc1LCJwdWIiOiJibHVlaG9sZSIsInRpdGxlIjoicHViZyIsImFwcCI6ImxvZ2tpbGwtcHViZyJ9.iWj4lA-Cjo3KxxRU1IOjWlskW2BvwqgVZV-7zebjGbw");
                httpClient.DefaultRequestHeaders.Add("AcceptEncoding", "gzip");
                return (httpClient, cts);
            }
            catch (HttpRequestException ex)
            {
                Debug.WriteLine($"HTTP request failed: {ex}");
                return new();
            }
            catch
            {
                await Shell.Current.DisplayAlertAsync("Timeout", "Requisição cancelada", "OK");
                Debug.WriteLine($"Requisição cancelada/timeout:");
                return new();
            }
        }
        public async Task<List<JogadorData>> BuscarPartidaDoJogador(string nome)
        {
            var (httpClient, tokenSource) = await CriarHttpClient();

            if (httpClient == null || tokenSource == null)
                return new();

            using (httpClient)
            using (tokenSource)
            {
                var response = await httpClient.GetAsync($"https://api.pubg.com/shards/steam/players?filter[playerNames]={nome}");
                if (!response.IsSuccessStatusCode) return new();

                var json = await response.Content.ReadAsStringAsync(tokenSource.Token);
                var PartidaDojogador = JsonConvert.DeserializeObject<JogadorResponser>(json);
                return PartidaDojogador?.Data?.Select(item => new JogadorData { Type = item.Type, Id = item.Id, Attributes = item.Attributes, Relationships = item.Relationships }).ToList() ?? new();
            }
        }
        public async Task<List<Matches>> BuscarDetalhesDaPartidas(List<JogadorData> PartidasDoJogador)
        {
            var partidas = new List<Matches>();

            var (httpClient, tokenSource) = await CriarHttpClient();

            using (httpClient)
            using (tokenSource)
            {
                var matchList = PartidasDoJogador.FirstOrDefault()?.Relationships?.Matches?.Data;
                if (matchList == null) return partidas;

                foreach (var match in matchList)
                {
                    var response = await httpClient.GetAsync($"https://api.pubg.com/shards/steam/matches/{match.Id}");
                    if (!response.IsSuccessStatusCode) continue;

                    var json = await response.Content.ReadAsStringAsync(tokenSource.Token);
                    var dados = JsonConvert.DeserializeObject<Matches>(json);
                    if (dados != null) partidas.Add(dados);
                }

                //var telemetry = partidas[0].Included?.Where(t => t.Type == "asset").ToList();

                return partidas;
            }
        }

    }
}
