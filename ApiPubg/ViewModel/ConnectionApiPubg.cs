using ApiPubg.Models.SquadModel;
using ApiPubg.Models.SquadModel.MatchesModels;
using ApiPubg.Popups;
using ApiPubg.Services;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Maui.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls.Shapes;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace ApiPubg.ViewModel
{
    public partial class ConnectionApiPubg : ObservableObject
    {
        public ICommand CommandPicher { get; }

        private bool _isCheckedV = true;
        public bool IsCheckedV
        {
            get => _isCheckedV;
            set => SetProperty(ref _isCheckedV, value);
        }
        private bool _isCheckedK = false;
        public bool IsCheckedK
        {
            get => _isCheckedK;
            set => SetProperty(ref _isCheckedK, value);
        }
        private bool _isCheckedD = false;
        public bool IsCheckedD
        {
            get => _isCheckedD;
            set => SetProperty(ref _isCheckedD, value);
        }

        private AttributesDataMatches _pickerSelectedItem = null!;
        public AttributesDataMatches PickerSelectedItem
        {
            get => _pickerSelectedItem;
            set
            {
                SetProperty(ref _pickerSelectedItem, value);
                if (value != null)
                { IsvisibleBorder = true; CommandPicher.Execute(value); }
                else { IsvisibleBorder = false; }
            }
        }
        private string _msPesquisandoInicio = string.Empty;
        public string MsPesquisandoInicio
        {
            get => _msPesquisandoInicio;
            set => SetProperty(ref _msPesquisandoInicio, value);
        }

        private bool _msPopupGridIsVisible = false;
        public bool MsPopupGridIsVisible
        {
            get => _msPopupGridIsVisible;
            set => SetProperty(ref _msPopupGridIsVisible, value);
        }

        private int _msInforPartidaCount = 0;
        public int MsInforPartidaCount
        {
            get => _msInforPartidaCount;
            set => SetProperty(ref _msInforPartidaCount, value);
        }

        private bool _isEnabledButton = true;
        public bool IsEnabledButton
        {
            get => _isEnabledButton;
            set => SetProperty(ref _isEnabledButton, value);
        }
        private bool _isvisibleNomeResultadoData = false;
        public bool IsvisibleNomeResultadoData
        {
            get => _isvisibleNomeResultadoData;
            set => SetProperty(ref _isvisibleNomeResultadoData, value);
        }

        private bool _isvisibleBorder = false;
        public bool IsvisibleBorder
        {
            get => _isvisibleBorder;
            set => SetProperty(ref _isvisibleBorder, value);
        }
        private bool _btnAnimacao = false;
        public bool BtnAnimacao
        {
            get => _btnAnimacao;
            set => SetProperty(ref _btnAnimacao, value);
        }

        private decimal _btnAnimacaoScala = 1;
        public decimal BtnAnimacaoScala
        {
            get => _btnAnimacaoScala;
            set => SetProperty(ref _btnAnimacaoScala, value);
        }
        private NomeJogador _nomeJogador = null!;
        public NomeJogador NomeJogador
        {
            get => _nomeJogador;
            set => SetProperty(ref _nomeJogador, value);
        }

        private IDispatcherTimer? timer;
        private int Rank { get; set; }
        private string? IdSquad { get; set; }
        public bool Ganhou { get; set; }
        public bool NovoSquad { get; set; } = true;
        private bool JogadorCont { get; set; } = false;
        private Page Page = null!;
        int IndexPartida = 0;

        private ApiService ApiService;
        private List<Matches> Partidas { get; set; }

        public ObservableCollection<JogadorData> Jogador;

        public ObservableCollection<AttributesDataMatches> InformacoesDaPartidas { get; set; }

        public ObservableCollection<SquadJogadores> SquadJogadores { get; set; }

        public ConnectionApiPubg()
        {
            CommandPicher = new AsyncRelayCommand<AttributesDataMatches>(async item =>
            {
                await SelecionarPartidaPicher(item!);
            });
            ApiService = new();
            Partidas = new List<Matches>();
            NomeJogador = new NomeJogador();
            Jogador = new ObservableCollection<JogadorData>();
            InformacoesDaPartidas = new ObservableCollection<AttributesDataMatches>();
            SquadJogadores = new ObservableCollection<SquadJogadores>();

            _ = ApiService.CriarHttpClient();
        }

        private async Task SelecionarPartidaPicher(AttributesDataMatches item)
        {
            SquadJogadores.Clear();
            MsCarregandoDadosSqud();

            await Task.Delay(3000);
            await MontarSquads(Partidas, item.Index);
        }
        private void MsCarregandoDadosSqud()
        {
            MsPopupGridIsVisible = false;

            PopupAnimacao();
            AtivarAnimacao();

            MsPesquisandoInicio = "MONTANDO OS SQUADS ";
            MsInforPartidaCount = 0;
        }
        [RelayCommand]
        private async Task DadosJogadorPopup(Jogador jogador)
        {
            var popup = new JogadorPopup(jogador);
            var page = Application.Current?.Windows.Count > 0 ? Application.Current.Windows[0].Page : null;
            if (page != null)
            {
                await page.ShowPopupAsync(popup);
            }
        }
        [RelayCommand]
        private async Task PesquisarJogadorSquad(string jogador)
        {
            var page = Application.Current?.Windows.Count > 0 ? Application.Current.Windows[0].Page : null;

            if (page != null)
            {
                var ms = await page.DisplayAlertAsync(jogador, "Deseja pesquisar o jogagador acima ?", "Sim", "Não");
                if (ms)
                {
                    NomeJogador.Nome = jogador;
                    await PartidasDoPubg();
                }
            }
        }
        [RelayCommand]
        private async Task PartidasDoPubg()
        {
            var page = Application.Current?.Windows.Count > 0 ? Application.Current.Windows[0].Page : null;
            if (string.IsNullOrWhiteSpace(NomeJogador.Nome))
            {
                if (page != null)
                    await page.DisplayAlertAsync("Campo obrigatório.", "O nick do jogador não informado.", "OK");
                return;
            }

            ConfigInicioPartidasDoPubg();

            var PartidasDoJogador = await ApiService.BuscarPartidaDoJogador(NomeJogador?.Nome!);

            PopupAnimacao();
            AtivarAnimacao();

            foreach (var j in PartidasDoJogador) Jogador.Add(j);

            Partidas = await ApiService.BuscarDetalhesDaPartidas(PartidasDoJogador);
            foreach (var p in Partidas)
            {
                InformacoesDaPartidas.Add(new AttributesDataMatches
                {
                    Index = IndexPartida,
                    CreatedAt = p.Data?.Attributes!.CreatedAt,
                    DataPartida = Convert.ToDateTime(p.Data?.Attributes!.DataPartida),
                    Duration = Convert.ToInt16(p.Data?.Attributes!.Duration),
                    GameMode = p.Data?.Attributes!.GameMode,
                    MapName = p.Data?.Attributes!.MapName,
                    QuantidadeJogadores = p.Included!.Count
                });
                IndexPartida++;
            }
            if (InformacoesDaPartidas.Count == 0)
            {
                await Shell.Current.ClosePopupAsync();
                timer?.Stop();

                if (page != null)
                {
                    await page.DisplayAlertAsync(NomeJogador?.Nome, "Partidas não localizadas para esse nick", "sair");
                }

                NomeJogador?.Nome = string.Empty;
                IsEnabledButton = true;
                return;
            }
            ConfigFinalPartidasDoPubg();
        }
        private void ConfigInicioPartidasDoPubg()
        {
            IndexPartida = 0;
            IsEnabledButton = false;
            IsvisibleNomeResultadoData = false;
            NomeJogador.NomeResultData = string.Empty;
            MsPopupGridIsVisible = false;
            MsPesquisandoInicio = "BUSCANDO PARTIDAS ";

            BtnAnimacao = true;
            IsCheckedV = true;
            IsCheckedK = false;
            IsCheckedD = false;
            SquadJogadores.Clear();
            Jogador.Clear();
            InformacoesDaPartidas.Clear();
        }
        private async void ConfigFinalPartidasDoPubg()
        {
            MsInforPartidaCount = InformacoesDaPartidas.Count;
            MsPesquisandoInicio = "ENCONTRADAS ";
            timer?.Stop();

            await Task.Delay(2000);
            await Shell.Current.ClosePopupAsync();

            IsEnabledButton = true;
            IsvisibleNomeResultadoData = true;
            NomeJogador?.NomeResultData = NomeJogador.Nome;
            NomeJogador?.Nome = string.Empty;
            BtnAnimacaoScala = 1;
            BtnAnimacao = false;
        }
        private async Task MontarSquads(List<Matches> partidas, int indexPartida)
        {
            if (partidas.Count == 0) return;

            var partidaDataAttributes = partidas?[indexPartida]?.Data?.Attributes;

            var PartidaIncluded = partidas?[indexPartida].Included;
            await AgruparPorSquad(PartidaIncluded!);
        }
        private async Task AgruparPorSquad(List<MatchesIncluded> PartidaIncluded)
        {
            await Task.Yield();

            var NomeJogador = PartidaIncluded?
           .Where(n => n.Attributes?.Stats?.Name != null)
           .OrderBy(o => o.Attributes?.Stats?.Name)
           .Select(n => new { n.Attributes?.Stats?.Name, n.Attributes?.Stats, n.Id }).ToList();

            var IncludedSquad = PartidaIncluded?
                .Where(s => s.Type == "roster").ToList();

            if (NomeJogador == null) return;
            foreach (var Nome in NomeJogador)
            {
                NovoSquad = true;

                if (IncludedSquad == null) return;
                foreach (var Squad in IncludedSquad)
                {
                    Rank = Convert.ToInt16(Squad?.Attributes?.Stats?.Rank);
                    Ganhou = Convert.ToBoolean(Squad?.Attributes?.Won);
                    IdSquad = Squad?.Id;

                    if (Squad?.RelationShips?.Participants?.Data == null) return;
                    foreach (var SquadNome in Squad.RelationShips.Participants.Data)
                    {

                        if (Nome?.Id == SquadNome.Id)
                        {
                            if (SquadJogadores != null && !SquadJogadores.Any())
                            {
                                SquadJogadores?.Add(new SquadJogadores
                                {
                                    Ganhou = Ganhou,
                                    Id = IdSquad,
                                    Rank = Rank,

                                    TotalKill = Convert.ToInt16(Nome?.Stats?.Kills),
                                    TotalDerrubado = Convert.ToInt16(Nome?.Stats?.Dbnos),
                                    TotalAssist = Convert.ToInt16(Nome?.Stats?.Assists),
                                    TotalDano = Convert.ToDecimal(Nome?.Stats?.DamageDealt),
                                    Jogador = new List<Jogador> { new Jogador
                                {
                                        NomeJogador = Nome?.Stats?.Name,
                                        Dbnos = Convert.ToInt16(Nome?.Stats?.Dbnos),
                                        Assists = Convert.ToInt16(Nome?.Stats?.Assists),
                                        boosts = Convert.ToInt16(Nome?.Stats?.boosts),
                                        DamageDealt = Convert.ToInt16(Nome?.Stats?.DamageDealt),
                                        Heals =Convert.ToInt16(Nome?.Stats?.Heals),
                                        DeathType = Nome?.Stats?.DeathType,
                                        HeadshotKills = Convert.ToInt16(Nome?.Stats?.HeadshotKills),
                                        KillStreaks = Convert.ToInt16(Nome?.Stats?.KillStreaks),
                                        KillPlace = Convert.ToInt16(Nome?.Stats?.KillPlace),
                                        Kills = Convert.ToInt16(Nome?.Stats?.Kills),
                                        LongestKill = Convert.ToDecimal(Nome?.Stats?.LongestKill),
                                        WinPlace = Convert.ToInt16( Nome?.Stats?.WinPlace),
                                        WalkDistance = Convert.ToInt16(Nome?.Stats?.WalkDistance),
                                        TimeSurvived = Convert.ToInt16(Nome?.Stats?.TimeSurvived),
                                        WeaponsAcquired = Convert.ToInt16( Nome?.Stats?.WeaponsAcquired),
                                        VehicleDestroys = Convert.ToInt16( Nome?.Stats?.VehicleDestroys)
                                 }}
                                }
                                );
                                NovoSquad = false;
                            }
                            else
                            {
                                for (int x = 0; x < SquadJogadores?.Count; x++)
                                {
                                    var Id = SquadJogadores[x].Id;
                                    if (Squad.Id == Id)
                                    {
                                        SquadJogadores?[x].TotalKill = Convert.ToInt16(SquadJogadores?[x].TotalKill) + Convert.ToInt16(Nome?.Stats?.Kills);
                                        SquadJogadores?[x].TotalDerrubado = Convert.ToInt16(SquadJogadores?[x].TotalDerrubado) + Convert.ToInt16(Nome?.Stats?.Dbnos);
                                        SquadJogadores?[x].TotalAssist = Convert.ToInt16(SquadJogadores?[x].TotalAssist) + Convert.ToInt16(Nome?.Stats?.Assists);
                                        SquadJogadores?[x].TotalDano = Convert.ToDecimal(SquadJogadores?[x].TotalDano) + Convert.ToInt16(Nome?.Stats?.DamageDealt);

                                        SquadJogadores?[x].Jogador?.Add(new Jogador
                                        {
                                            NomeJogador = Nome?.Stats?.Name,
                                            Dbnos = Convert.ToInt16(Nome?.Stats?.Dbnos),
                                            Assists = Convert.ToInt16(Nome?.Stats?.Assists),
                                            boosts = Convert.ToInt16(Nome?.Stats?.boosts),
                                            DamageDealt = Convert.ToInt16(Nome?.Stats?.DamageDealt),
                                            Heals = Convert.ToInt16(Nome?.Stats?.Heals),
                                            DeathType = Nome?.Stats?.DeathType,
                                            HeadshotKills = Convert.ToInt16(Nome?.Stats?.HeadshotKills),
                                            KillStreaks = Convert.ToInt16(Nome?.Stats?.KillStreaks),
                                            KillPlace = Convert.ToInt32(Nome?.Stats?.KillPlace),
                                            Kills = Convert.ToInt16(Nome?.Stats?.Kills),
                                            LongestKill = Convert.ToDecimal(Nome?.Stats?.LongestKill),
                                            WinPlace = Convert.ToInt16(Nome?.Stats?.WinPlace),
                                            WalkDistance = Convert.ToInt16(Nome?.Stats?.WalkDistance),
                                            TimeSurvived = Convert.ToInt16(Nome?.Stats?.TimeSurvived),
                                            WeaponsAcquired = Convert.ToInt16(Nome?.Stats?.WeaponsAcquired),
                                            VehicleDestroys = Convert.ToInt16(Nome?.Stats?.VehicleDestroys)

                                        });
                                        NovoSquad = false;
                                        break;
                                    }
                                }
                            }
                            JogadorCont = true;
                            break;
                        }
                    }
                    if (JogadorCont)
                    {
                        JogadorCont = false;
                        break;
                    }
                }
                if (NovoSquad)
                {
                    SquadJogadores?.Add(new SquadJogadores
                    {
                        Ganhou = Ganhou,
                        Id = IdSquad,
                        Rank = Rank,

                        TotalKill = Convert.ToInt16(Nome?.Stats?.Kills),
                        TotalDerrubado = Convert.ToInt16(Nome?.Stats?.Dbnos),
                        TotalAssist = Convert.ToInt16(Nome?.Stats?.Assists),
                        TotalDano = Convert.ToDecimal(Nome?.Stats?.DamageDealt),

                        Jogador = new List<Jogador>
                        { new Jogador
                    {
                       NomeJogador = Nome?.Stats?.Name,
                       Dbnos = Convert.ToInt16(Nome?.Stats?.Dbnos),
                       Assists = Convert.ToInt16(Nome?.Stats?.Assists),
                       boosts = Convert.ToInt16(Nome?.Stats?.boosts),
                       DamageDealt = Convert.ToInt16(Nome?.Stats?.DamageDealt),
                       Heals =Convert.ToInt16(Nome?.Stats?.Heals),
                       DeathType = Nome?.Stats?.DeathType,
                       HeadshotKills = Convert.ToInt16(Nome?.Stats?.HeadshotKills),
                       KillStreaks = Convert.ToInt16(Nome?.Stats?.KillStreaks),
                       KillPlace = Convert.ToInt16(Nome?.Stats?.KillPlace),
                       Kills = Convert.ToInt16(Nome?.Stats?.Kills),
                       LongestKill = Convert.ToDecimal(Nome?.Stats?.LongestKill),
                       WinPlace = Convert.ToInt16( Nome?.Stats?.WinPlace),
                       WalkDistance = Convert.ToInt16(Nome?.Stats?.WalkDistance),
                       TimeSurvived = Convert.ToInt16(Nome?.Stats?.TimeSurvived),
                       WeaponsAcquired = Convert.ToInt16( Nome?.Stats?.WeaponsAcquired),
                       VehicleDestroys = Convert.ToInt16( Nome?.Stats?.VehicleDestroys)
                    }
                        }
                    });
                }
            }

            OdernarSquadRank();

            timer?.Stop();
            MsPesquisandoInicio = "TODOS OS SQUADS MONTADOS ";
            MsPopupGridIsVisible = true;

            await Task.Delay(2000);
            await Shell.Current.ClosePopupAsync();
        }

        [RelayCommand]
        private void OdernarSquadRank()
        {
            List<SquadJogadores> ordernarSquadRank = [];

            if (IsCheckedV == true)
            {
                ordernarSquadRank = SquadJogadores
                   .OrderBy(o => o.Rank)
                   .Select(o => new SquadJogadores
                   {
                       Rank = o.Rank,
                       TotalKill = o.TotalKill,
                       TotalDerrubado = o.TotalDerrubado,
                       Id = o.Id,
                       Ganhou = o.Ganhou,
                       TotalDano = o.TotalDano,
                       TotalAssist = o.TotalAssist,
                       TeamId = o.TeamId,
                       Jogador = o.Jogador?.OrderBy(a => a.WinPlace).ThenBy(t => t.NomeJogador).ToList()
                   }).ToList();
            }

            if (IsCheckedK == true)
            {
                ordernarSquadRank = SquadJogadores
                   .OrderByDescending(k => k.TotalKill)
                   .Select(k => new SquadJogadores
                   {
                       Rank = k.Rank,
                       TotalKill = k.TotalKill,
                       TotalDerrubado = k.TotalDerrubado,
                       Id = k.Id,
                       Ganhou = k.Ganhou,
                       TotalDano = k.TotalDano,
                       TotalAssist = k.TotalAssist,
                       TeamId = k.TeamId,
                       Jogador = k.Jogador?.OrderByDescending(k => k.Kills).ThenBy(k => k.NomeJogador).ToList()
                   }).ToList();
            }

            if (IsCheckedD == true)
            {
                ordernarSquadRank = SquadJogadores
                   .OrderByDescending(d => d.TotalDano)
                   .Select(d => new SquadJogadores
                   {
                       Rank = d.Rank,
                       TotalKill = d.TotalKill,
                       TotalDerrubado = d.TotalDerrubado,
                       Id = d.Id,
                       Ganhou = d.Ganhou,
                       TotalDano = d.TotalDano,
                       TotalAssist = d.TotalAssist,
                       TeamId = d.TeamId,
                       Jogador = d.Jogador?.OrderByDescending(d => d.DamageDealt).ThenBy(d => d.NomeJogador).ToList()
                   }).ToList();
            }

            SquadJogadores?.Clear();
            var posicaoSquad = 1;

            foreach (var squad in ordernarSquadRank!)
            {
                squad.Posicao = posicaoSquad;
                var index = squad.Jogador?.Count;

                for (int x = 0; x < index; x++)
                    squad.Jogador?[x].index = x + 1;

                SquadJogadores?.Add(squad);
                posicaoSquad++;
            }
        }
        private async void PopupAnimacao()
        {
            var popup = new PesquisaSquadPopups(this);

            await Shell.Current.ShowPopupAsync(popup, new PopupOptions
            {
                CanBeDismissedByTappingOutsideOfPopup = false,
                Shape = new RoundRectangle
                {
                    CornerRadius = new CornerRadius(20),
                    Stroke = new SolidColorBrush(Colors.AliceBlue),
                    StrokeThickness = 0,
                },
            });
        }
        private void AtivarAnimacao()
        {
            var ms = MsPesquisandoInicio;

            var dispatcher = Application.Current?.Dispatcher;
            if (dispatcher != null)
            {
                timer = dispatcher.CreateTimer();
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += (s, e) =>
                {
                    if (BtnAnimacaoScala == 1)
                        BtnAnimacaoScala = 0.8m;
                    else
                    if (BtnAnimacaoScala == 0.8m)
                    {
                        BtnAnimacaoScala = 1.2m;
                    }
                    else
                        if (BtnAnimacaoScala == 1.2m)
                    {
                        BtnAnimacaoScala = 1;
                    }
                    MsPesquisandoInicio += ".";
                    if (MsPesquisandoInicio.Length > 30)
                    {
                        MsPesquisandoInicio = ms;
                    }
                };
                timer.Start();
            }
        }
    }
}
