using ApiPubg.Models.Painel;
using ApiPubg.Models.ServicesModel;
using ApiPubg.Models.SquadModel;
using ApiPubg.Models.SquadModel.MatchesModels;
using ApiPubg.Models.TelemetriaModel;
using ApiPubg.Services;
using ApiPubg.Services.Databases;
using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls.PlatformConfiguration;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ApiPubg.ViewModel
{
    public partial class PainelViewModel : ObservableObject
    {
        private DadosJogadorModel _idDadosjogador = null!;
        public DadosJogadorModel IdDadosJogador
        {
            get => _idDadosjogador;
            set => SetProperty(ref _idDadosjogador, value);
        }

        private DerrubouVictimModel _derrubouVictimModel = null!;
        public DerrubouVictimModel DerrubouVictimModel
        {
            get => _derrubouVictimModel;
            set => SetProperty(ref _derrubouVictimModel, value);
        }

        private FinalizouVictimModel _FinalizouVictimModel = null!;
        public FinalizouVictimModel FinalizouVictimModel
        {
            get => _FinalizouVictimModel;
            set => SetProperty(ref _FinalizouVictimModel, value);
        }

        //private DerrubouModel _derrubouModel = null!;
        //public DerrubouModel DerrubouModel
        //{
        //    get => _derrubouModel;
        //    set => SetProperty(ref _derrubouModel, value);
        //}

        //private FinalizouModel _FinalizouModel = null!;
        //public FinalizouModel FinalizouModel
        //{
        //    get => _FinalizouModel;
        //    set => SetProperty(ref _FinalizouModel, value);
        //}

        private string _resultNome = string.Empty;
        public string ResultNome
        {
            get => _resultNome;
            set => SetProperty(ref _resultNome, value);
        }
        private int _resultCont = 0;
        public int ResultCont
        {
            get => _resultCont;
            set => SetProperty(ref _resultCont, value);
        }
        private string _nickJogador = string.Empty;
        public string NickJogador
        {
            get => _nickJogador;
            set => SetProperty(ref _nickJogador, value);
        }
        private bool _isEnabledButtom = false;
        public bool IsEnabledButtom
        {
            get => _isEnabledButtom;
            set => SetProperty(ref _isEnabledButtom, value);
        }
        private DateTime? _selectedData = DateTime.Today;
        public DateTime? SelectedData
        {
            get => _selectedData;
            set => SetProperty(ref _selectedData, value);
        }

        private bool? _valorRadioDataT = true;
        public bool? ValorRadioDataT
        {
            get => _valorRadioDataT;
            set
            {
                SetProperty(ref _valorRadioDataT, value);
                SelectedData = DateTime.Today;
            }
        }

        private bool? _valorRadioDataD = false;
        public bool? ValorRadioDataD
        {
            get => _valorRadioDataD;
            set
            {
                SetProperty(ref _valorRadioDataD, value);
                IsEnabledButtom = true;
            }
        }

        private DadosJogadorPainel _dadosJogadorPainel = null!;
        public DadosJogadorPainel DadosJogadorPainel
        {
            get => _dadosJogadorPainel;
            set => SetProperty(ref _dadosJogadorPainel, value);
        }

        private readonly string dbPath = System.IO.Path.Combine(FileSystem.AppDataDirectory, "DadosJogador");


        //private int IndexPartida;
        private ApiTelemetry _apiTelemetry;
        public DatabaseDadosJogador DatabaseDados;
        //public DatabaseTelemetry DatabaseTelemetry;
        private List<AttributesIncludedMatches> _partidas { get; set; }
        //public ObservableCollection<RootModel> Telemetry { get; set; }

        public ObservableCollection<JogadorData> Jogador;
        public ObservableCollection<DadosJogadorModel> DadosJogador { get; set; }
        public ObservableCollection<AttributesDataMatches> InformacoesDaPartidas { get; set; }
        public ObservableCollection<DerrubouModel> DerrubouModelCollection { get; set; }
        public ObservableCollection<FinalizouModel> FinalizouModelCollection { get; set; }


        private ApiService _apiService;

        public PainelViewModel()
        {
            //TelemetryDerrubou = new TelemetryDerrubou();
            DadosJogadorPainel = new DadosJogadorPainel();
            _apiTelemetry = new ApiTelemetry();
            DatabaseDados = new DatabaseDadosJogador(dbPath);
            //DatabaseTelemetry = new DatabaseTelemetry(dbPathTelemetry);
            _partidas = new List<AttributesIncludedMatches>();
            //Telemetry = new ObservableCollection<RootModel>();
            DerrubouVictimModel = new DerrubouVictimModel();
            FinalizouVictimModel = new FinalizouVictimModel();
            Jogador = new ObservableCollection<JogadorData>();
            DadosJogador = new ObservableCollection<DadosJogadorModel>();
            InformacoesDaPartidas = new ObservableCollection<AttributesDataMatches>();
            DerrubouModelCollection = new ObservableCollection<DerrubouModel>();
            FinalizouModelCollection = new ObservableCollection<FinalizouModel>();
            _apiService = new ApiService();
        }

        [RelayCommand]
        private async Task PartidasDoPubg()
        {
            var partidasDoJogador = await _apiService.BuscarPartidaDoJogador(NickJogador.Trim());

            foreach (var j in partidasDoJogador) Jogador.Add(j);

            var partidas = await _apiService.BuscarDetalhesDaPartidas(partidasDoJogador);

            foreach (var p in partidas)
            {
                var createdAtData = DateTime.TryParse(p.Data?.Attributes!.CreatedAt, out var dt) ? dt.ToString("dd/MM/yyyy") : string.Empty;
                var createdAtHora = DateTime.TryParse(p.Data?.Attributes!.CreatedAt, out var hr) ? hr.ToString("HH:mm:ss") : string.Empty;

                var createdDH = DateTime.TryParse(p.Data?.Attributes!.CreatedAt, out var dh) ? hr.ToString("dd/MM/yyyy HH:mm:ss") : string.Empty;
                var crearted_dh = DateTime.ParseExact(createdDH, "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

                var telemetry = p.Included?.Where(t => t.Type == "asset").Select(t => t.Attributes?.URL).ToList();
                var telemetryString = telemetry?.FirstOrDefault();

                foreach (var item in p.Included!)
                {
                    if (item.Attributes?.Stats?.Name != null && item.Attributes.Stats.Name.Contains(NickJogador.Trim()))
                    {
                        _partidas.Add(new AttributesIncludedMatches { Stats = item.Attributes.Stats });

                        var jogador = await DatabaseDados.GetDadosJogadorAsync();
                        var jogadorData = jogador.Where(j => j.Data == createdAtData).Select(j => j.Data).FirstOrDefault();
                        var jogadorHora = jogador.Where(j => j.Hora == createdAtHora).Select(j => j.Hora).FirstOrDefault();

                        var addDados = true;

                        if (!string.IsNullOrEmpty(createdAtData))
                            if (!string.IsNullOrEmpty(jogadorData))
                            {
                                if (!string.IsNullOrEmpty(jogadorHora))
                                {
                                    addDados = false;
                                }
                            }

                        if (addDados)
                            await DatabaseDados.AddDadosJogadorAsync(new DadosJogadorModel
                            {
                                Data = createdAtData!,
                                Hora = createdAtHora!,
                                DH = crearted_dh,
                                Telemetry = telemetryString,
                                Kills = item.Attributes.Stats.Kills,
                                RideDistance = item.Attributes.Stats.RideDistance,
                                Assists = item.Attributes.Stats.Assists,
                                boosts = item.Attributes.Stats.boosts,
                                DamageDealt = item.Attributes.Stats.DamageDealt,
                                Dbnos = item.Attributes.Stats.Dbnos,
                                DeathType = item.Attributes.Stats.DeathType,
                                HeadshotKills = item.Attributes.Stats.HeadshotKills,
                                Heals = item.Attributes.Stats.Heals,
                                KillPlace = item.Attributes.Stats.KillPlace,
                                KillStreaks = item.Attributes.Stats.KillStreaks,
                                LongestKill = item.Attributes.Stats.LongestKill,
                                Rank = item.Attributes.Stats.Rank,
                                Revives = item.Attributes.Stats.Revives,
                                RoadKills = item.Attributes.Stats.RoadKills,
                                SwimDistance = item.Attributes.Stats.SwimDistance,
                                TeamKills = item.Attributes.Stats.TeamKills,
                                TimeSurvived = item.Attributes.Stats.TimeSurvived,
                                VehicleDestroys = item.Attributes.Stats.VehicleDestroys,
                                WalkDistance = item.Attributes.Stats.WalkDistance,
                                WeaponsAcquired = item.Attributes.Stats.WeaponsAcquired,
                                WinPlace = item.Attributes.Stats.WinPlace
                            });
                    }
                }
            }

            var dataHoraControle = await DatabaseDados.GetDataHoraControleAsync();
            var DHcontrole = dataHoraControle.Select(dh => dh.DH).OrderDescending().ToList();

            var dados = await DatabaseDados.GetDadosJogadorAsync();
            var dadosAtualizados = DHcontrole?.Count == 0 ? dados : dados.Where(d => d.DH > DHcontrole?[0]).ToList();

            if (dadosAtualizados.Any())
            {
                await Shell.Current.DisplayAlertAsync("TELEMETRY", $"Foi encontrdo {dadosAtualizados.Count}", "Atualizar");
            }
            else await Shell.Current.DisplayAlertAsync("TELEMETRY", $" Não foi encontrdo dados para ser atualizado", "OK");

            if (dadosAtualizados.Any())
                foreach (var item in dadosAtualizados)
                {
                    await DatabaseDados.AddDataHoraControleAsync(new DataHoraControle { DH = item.DH });

                    var resultGeral = await _apiTelemetry.BuscarTelemetry(item.Telemetry ?? string.Empty);
                    //var resultAttacker = resultGeral.Where(r => r.Attacker?.Name == "NibiruSun" && r.Victim?.Name != "NibiruSun" &&  r.Victim != null && r._T != "LogPlayerTakeDamage").ToList();

                    var resultVictim = resultGeral.Where(r => r.Victim?.Name == "NibiruSun" && r._T == "LogPlayerKillV2").ToList();
                    var primeiroVictimGeral = resultVictim.Select(d => new { d.Derrubou, d._D, d.InforDerrubou, d.Finalizou, d.InforFinalizou }).FirstOrDefault();
                    var dataVictim = DateTime.TryParse(primeiroVictimGeral?._D, out var dt) ? dt.ToString("dd/MM/yyyy") : string.Empty;
                    var horaVictim = DateTime.TryParse(primeiroVictimGeral?._D, out var hr) ? hr.ToString("HH:mm:ss") : string.Empty;

                    var resultDerrubou = resultGeral.Where(r => r.Derrubou?.Name == "NibiruSun" && r._T == "LogPlayerKillV2").ToList();
                    //var primeiroDerrubou = resultDerrubou.Select(d => new { d.Derrubou, d._D, d.InforDerrubou, d.Finalizou, d.InforFinalizou }).FirstOrDefault();
                    //var dataDerrubou = DateTime.TryParse(primeiroDerrubou?._D, out var dtd) ? dtd.ToString("dd/MM/yyyy") : string.Empty;
                    //var horaDerrubou = DateTime.TryParse(primeiroDerrubou?._D, out var hrd) ? hrd.ToString("HH:mm:ss") : string.Empty;

                    var resultFinalizou = resultGeral.Where(r => r.Finalizou?.Name == "NibiruSun" && r._T == "LogPlayerKillV2").ToList();
                    //var primeiroFinalizou = resultFinalizou.Select(d => new { d.Derrubou, d._D, d.InforDerrubou, d.Finalizou, d.InforFinalizou }).FirstOrDefault();
                    //var dataFinalizou = DateTime.TryParse(primeiroFinalizou?._D, out var dtf) ? dtf.ToString("dd/MM/yyyy") : string.Empty;
                    //var horaFinalizou = DateTime.TryParse(primeiroFinalizou?._D, out var hrf) ? hrf.ToString("HH:mm:ss") : string.Empty;

                    #region Dados Victim Derrubou
                    var databaseDadosVictimDerrubou = await DatabaseDados.GetTelemetryVictimDerrubouAsync();
                    var verDataVictimDerrubou = databaseDadosVictimDerrubou.Select(d => d.Data).ToList();
                    var verHoraVictimDerrubou = databaseDadosVictimDerrubou.Select(d => d.Hora).ToList();

                    if (primeiroVictimGeral?.Derrubou != null)

                        if (!verDataVictimDerrubou.Contains(dataVictim))
                        {
                            await DatabaseDados.AddTelemetryVictimDerrubouAsync(new TelemetryVictimDerrubou
                            {
                                Name = primeiroVictimGeral.Derrubou?.Name,
                                Health = primeiroVictimGeral.Derrubou?.Health ?? 0f,
                                DamageCauserName = primeiroVictimGeral.InforDerrubou?.DamageCauserName,
                                DamageReason = primeiroVictimGeral.InforDerrubou?.DamageReason,
                                DamageTypeCategory = primeiroVictimGeral.InforDerrubou?.DamageTypeCategory,
                                Distanece = primeiroVictimGeral.InforDerrubou?.Distanece ?? 0f,
                                IdDadosJogador = item.Id,
                                Data = dataVictim,
                                Hora = horaVictim
                            });
                        }
                        else
                            if (!verHoraVictimDerrubou.Contains(horaVictim))
                                await DatabaseDados.AddTelemetryVictimDerrubouAsync(new TelemetryVictimDerrubou
                                {
                                    Name = primeiroVictimGeral.Derrubou?.Name,
                                    Health = primeiroVictimGeral.Derrubou?.Health ?? 0f,
                                    DamageCauserName = primeiroVictimGeral.InforDerrubou?.DamageCauserName,
                                    DamageReason = primeiroVictimGeral.InforDerrubou?.DamageReason,
                                    DamageTypeCategory = primeiroVictimGeral.InforDerrubou?.DamageTypeCategory,
                                    Distanece = primeiroVictimGeral.InforDerrubou?.Distanece ?? 0f,
                                    IdDadosJogador = item.Id,
                                    Data = dataVictim,
                                    Hora = horaVictim
                                });
                    #endregion

                    #region Dados Victim Finalizou

                    var databaseDadosVictimFinalizou = await DatabaseDados.GetTelemetryVictimFinalizouAsync();

                    var verDataVictimFinalizou = databaseDadosVictimFinalizou.Select(d => d.Data).ToList();
                    var verHoraVictimFinalizou = databaseDadosVictimFinalizou.Select(d => d.Hora).ToList();

                    if (primeiroVictimGeral?.Finalizou != null)
                        if (!verDataVictimFinalizou.Contains(dataVictim))
                        {
                            await DatabaseDados.AddTelemetryVictimFinalizouAsync(new TelemetryVictimFinalizou
                            {
                                Name = primeiroVictimGeral?.Finalizou?.Name,
                                Health = primeiroVictimGeral?.Finalizou?.Health ?? 0f,
                                DamageCauserName = primeiroVictimGeral?.InforFinalizou?.DamageCauserName,
                                DamageReason = primeiroVictimGeral?.InforFinalizou?.DamageReason,
                                DamageTypeCategory = primeiroVictimGeral?.InforFinalizou?.DamageTypeCategory,
                                Distanece = primeiroVictimGeral?.InforFinalizou?.Distanece ?? 0f,
                                IdDadosJogador = item.Id,
                                Data = dataVictim,
                                Hora = horaVictim
                            });
                        }
                        else
                            if (!verHoraVictimFinalizou.Contains(horaVictim))
                                await DatabaseDados.AddTelemetryVictimFinalizouAsync(new TelemetryVictimFinalizou
                                {
                                    Name = primeiroVictimGeral?.Finalizou?.Name,
                                    Health = primeiroVictimGeral?.Finalizou?.Health ?? 0f,
                                    DamageCauserName = primeiroVictimGeral?.InforFinalizou?.DamageCauserName,
                                    DamageReason = primeiroVictimGeral?.InforFinalizou?.DamageReason,
                                    DamageTypeCategory = primeiroVictimGeral?.InforFinalizou?.DamageTypeCategory,
                                    Distanece = primeiroVictimGeral?.InforFinalizou?.Distanece ?? 0f,
                                    IdDadosJogador = item.Id,
                                    Data = dataVictim,
                                    Hora = horaVictim
                                });
                    #endregion

                    #region Dados Jogador Derrubou
                    var databaseDadosDerrubou = await DatabaseDados.GetTelemetryDerrubouAsync();
                    var verDataDerrubou = databaseDadosDerrubou.Select(d => d.Data).ToList();
                    var verHoraDerrubou = databaseDadosDerrubou.Select(d => d.Hora).ToList();

                    foreach (var ItemDerrubou in resultDerrubou)
                    {
                        //var primeiroDerrubou = ItemDerrubou.Select(d => new { d.Derrubou, d._D, d.InforDerrubou, d.Finalizou, d.InforFinalizou }).FirstOrDefault();
                        var dataDerrubou = DateTime.TryParse(ItemDerrubou?._D, out var dtd) ? dtd.ToString("dd/MM/yyyy") : string.Empty;
                        var horaDerrubou = DateTime.TryParse(ItemDerrubou?._D, out var hrd) ? hrd.ToString("HH:mm:ss") : string.Empty;

                        if (ItemDerrubou?.Derrubou != null)
                            if (!verDataDerrubou.Contains(dataDerrubou))
                            {
                                await DatabaseDados.AddTelemetryDerrubouAsync(new TelemetryDerrubou
                                {
                                    Name = ItemDerrubou.Victim?.Name,
                                    Health = ItemDerrubou.Victim?.Health ?? 0f,
                                    DamageCauserName = ItemDerrubou.InforDerrubou?.DamageCauserName,
                                    DamageReason = ItemDerrubou.InforDerrubou?.DamageReason,
                                    DamageTypeCategory = ItemDerrubou.InforDerrubou?.DamageTypeCategory,
                                    Distanece = ItemDerrubou.InforDerrubou?.Distanece ?? 0f,
                                    IdDadosJogador = item.Id,
                                    Data = dataDerrubou,
                                    Hora = horaDerrubou
                                });
                            }
                            else
                                if (!verHoraDerrubou.Contains(horaDerrubou))
                                    await DatabaseDados.AddTelemetryDerrubouAsync(new TelemetryDerrubou
                                    {
                                        Name = ItemDerrubou.Victim?.Name,
                                        Health = ItemDerrubou.Victim?.Health ?? 0f,
                                        DamageCauserName = ItemDerrubou.InforDerrubou?.DamageCauserName,
                                        DamageReason = ItemDerrubou.InforDerrubou?.DamageReason,
                                        DamageTypeCategory = ItemDerrubou.InforDerrubou?.DamageTypeCategory,
                                        Distanece = ItemDerrubou.InforDerrubou?.Distanece ?? 0f,
                                        IdDadosJogador = item.Id,
                                        Data = dataDerrubou,
                                        Hora = horaDerrubou
                                    });
                    }

                    #endregion

                    #region Dados Jogador Finalizou
                    var databaseDadosFinalizou = await DatabaseDados.GetTelemetryFinalizouAsync();
                    var verDataFinalizou = databaseDadosFinalizou.Select(d => d.Data).ToList();
                    var verHoraFinalizou = databaseDadosFinalizou.Select(d => d.Hora).ToList();

                    foreach (var itemFinalizou in resultFinalizou)
                    {
                        var dataFinalizou = DateTime.TryParse(itemFinalizou?._D, out var dtf) ? dtf.ToString("dd/MM/yyyy") : string.Empty;
                        var horaFinalizou = DateTime.TryParse(itemFinalizou?._D, out var hrf) ? hrf.ToString("HH:mm:ss") : string.Empty;

                        if (itemFinalizou?.Finalizou != null)
                            if (!verDataFinalizou.Contains(dataFinalizou))
                            {
                                await DatabaseDados.AddTelemetryFinalizouAsync(new TelemetryFinalizou
                                {
                                    Name = itemFinalizou.Victim?.Name,
                                    Health = itemFinalizou.Victim?.Health ?? 0f,
                                    DamageCauserName = itemFinalizou.InforFinalizou?.DamageCauserName,
                                    DamageReason = itemFinalizou.InforFinalizou?.DamageReason,
                                    DamageTypeCategory = itemFinalizou.InforFinalizou?.DamageTypeCategory,
                                    Distanece = itemFinalizou.InforFinalizou?.Distanece ?? 0f,
                                    IdDadosJogador = item.Id,
                                    Data = dataFinalizou,
                                    Hora = horaFinalizou
                                });
                            }
                            else
                                if (!verHoraFinalizou.Contains(horaFinalizou))
                                    await DatabaseDados.AddTelemetryFinalizouAsync(new TelemetryFinalizou
                                    {
                                        Name = itemFinalizou.Victim?.Name,
                                        Health = itemFinalizou.Victim?.Health ?? 0f,
                                        DamageCauserName = itemFinalizou.InforFinalizou?.DamageCauserName,
                                        DamageReason = itemFinalizou.InforFinalizou?.DamageReason,
                                        DamageTypeCategory = itemFinalizou.InforFinalizou?.DamageTypeCategory,
                                        Distanece = itemFinalizou.InforFinalizou?.Distanece ?? 0f,
                                        IdDadosJogador = item.Id,
                                        Data = dataFinalizou,
                                        Hora = horaFinalizou
                                    });
                    }

                    #endregion
                }
            foreach (var item in dados)
                DadosJogador.Add(item);
            TotalizarPainel(dados);

        }
        private void TotalizarPainel(List<DadosJogadorModel> dados)
        {
            DadosJogadorPainel.Primeiro = dados.Count(d => d.WinPlace == 1);
            DadosJogadorPainel.Segundo = dados.Count(d => d.WinPlace == 2);
            DadosJogadorPainel.Terceiro = dados.Count(d => d.WinPlace == 3);
            DadosJogadorPainel.Quarto = dados.Count(d => d.WinPlace == 4);
            DadosJogadorPainel.Quinto = dados.Count(d => d.WinPlace == 5);
            DadosJogadorPainel.Sexto = dados.Count(d => d.WinPlace == 6);
            DadosJogadorPainel.Setimo = dados.Count(d => d.WinPlace == 7);
            DadosJogadorPainel.Oitavo = dados.Count(d => d.WinPlace == 8);
            DadosJogadorPainel.Nono = dados.Count(d => d.WinPlace == 9);
            DadosJogadorPainel.Decimo = dados.Count(d => d.WinPlace == 10);
            DadosJogadorPainel.QtdPartidas = dados.Count;
            DadosJogadorPainel.kills = dados.Sum(d => d.Kills);
            DadosJogadorPainel.Dbnos = dados.Sum(d => d.Dbnos);
            DadosJogadorPainel.Asssits = dados.Sum(d => d.Assists);
            DadosJogadorPainel.DamageDealt = dados.Sum(d => d.DamageDealt);
            DadosJogadorPainel.LongestKill = dados.Select(d => d.LongestKill).DefaultIfEmpty().Max();
            // Soma o tempo total em horas, convertendo a string Hora para TimeSpan e somando os minutos
            decimal totalMinutos = dados
                .Where(a => !string.IsNullOrEmpty(a.Hora))
                .Select(a =>
                {
                    TimeSpan ts;
                    if (TimeSpan.TryParse(a.Hora, out ts))
                        return (decimal)ts.TotalMinutes;
                    return 0m;
                })
                .Sum();
            DadosJogadorPainel.TotalHora = Math.Round((totalMinutos / 60) / 60, 2);

        }

        [RelayCommand]
        private async Task PesquisarData()
        {
            var dados = await DatabaseDados.GetDadosJogadorAsync();

            if (ValorRadioDataT != null)
                if ((bool)ValorRadioDataT)
                {
                    DerrubouVictimModel.Name = null;
                    FinalizouVictimModel.Name = null;

                    IsEnabledButtom = false;
                    DadosJogador.Clear();
                    await Shell.Current.DisplayAlertAsync("Todas", $"{dados.Count} partidas localizadas", "OK");

                    foreach (var item in dados)
                        DadosJogador.Add(item);
                }
            TotalizarPainel(dados);


            if (ValorRadioDataD != null)
                if ((bool)ValorRadioDataD)
                {

                    var dataConvert = SelectedData?.ToString("dd/MM/yyyy") ?? string.Empty;
                    var dadosDataD = dados.Where(d => d.Data == dataConvert).ToList();
                    if (!dadosDataD.Any())
                    {
                        await Shell.Current.DisplayAlertAsync($"{dataConvert}", "Não foi encontrado partidas.", "Ok");
                        return;
                    }

                    DerrubouVictimModel.Name = null;
                    FinalizouVictimModel.Name = null;

                    DadosJogador.Clear();
                    await Shell.Current.DisplayAlertAsync($"{dataConvert}", $"{dadosDataD.Count} partidas localizadas\"", "OK");
                    foreach (var item in dadosDataD)
                        DadosJogador.Add(item);
                    TotalizarPainel(dadosDataD);
                }
        }

        [RelayCommand]
        private async Task GetTelemetry()
        {
            #region Victim derrubou finalizou
            var getTelemetryVictimDerrubou = await DatabaseDados.GetTelemetryVictimDerrubouAsync();
            var resultIdVictimDerrubou = getTelemetryVictimDerrubou.Where(t => t.IdDadosJogador == IdDadosJogador.Id).FirstOrDefault();

            DerrubouVictimModel.Name = resultIdVictimDerrubou?.Name ?? string.Empty;
            DerrubouVictimModel.Health = resultIdVictimDerrubou?.Health ?? 0f;
            DerrubouVictimModel.Distance = (resultIdVictimDerrubou?.Distanece / 100) ?? 0f;
            DerrubouVictimModel.DamageCauserName = resultIdVictimDerrubou?.DamageCauserName;
            DerrubouVictimModel.DamageReason = resultIdVictimDerrubou?.DamageReason;
            DerrubouVictimModel.DamageTypeCategory = resultIdVictimDerrubou?.DamageTypeCategory;

            var getTelemetryVictimFinalizou = await DatabaseDados.GetTelemetryVictimFinalizouAsync();
            var resultIdVictimFinalizou = getTelemetryVictimFinalizou.Where(t => t.IdDadosJogador == IdDadosJogador.Id).FirstOrDefault();

            FinalizouVictimModel.Name = resultIdVictimFinalizou?.Name ?? string.Empty;
            FinalizouVictimModel.Health = resultIdVictimFinalizou?.Health ?? 0f;
            FinalizouVictimModel.Distance = (resultIdVictimFinalizou?.Distanece / 1000) ?? 0f;
            FinalizouVictimModel.DamageCauserName = resultIdVictimFinalizou?.DamageCauserName;
            FinalizouVictimModel.DamageReason = resultIdVictimFinalizou?.DamageReason;
            FinalizouVictimModel.DamageTypeCategory = resultIdVictimFinalizou?.DamageTypeCategory;
            #endregion

            #region Jogar derrubou finalizou
            var getTelemetryDerrubou = await DatabaseDados.GetTelemetryDerrubouAsync();
            var resultIdDerrubou = getTelemetryDerrubou.Where(t => t.IdDadosJogador == IdDadosJogador.Id).ToList();

            DerrubouModelCollection.Clear();
            foreach (var itemDerrubou in resultIdDerrubou)
            {
                DerrubouModelCollection.Add(new DerrubouModel
                {
                    Name = itemDerrubou?.Name ?? string.Empty,
                    Health = itemDerrubou?.Health ?? 0f,
                    Distance = (itemDerrubou?.Distanece / 100) ?? 0f,
                    DamageCauserName = itemDerrubou?.DamageCauserName,
                    DamageReason = itemDerrubou?.DamageReason,
                    DamageTypeCategory = itemDerrubou?.DamageTypeCategory
                });
            }

            var getTelemetryFinalizou = await DatabaseDados.GetTelemetryFinalizouAsync();
            var resultIdFinalizou = getTelemetryFinalizou.Where(t => t.IdDadosJogador == IdDadosJogador.Id).ToList();

            FinalizouModelCollection.Clear();
            foreach (var itemFinalizou in resultIdFinalizou)
            {
                FinalizouModelCollection.Add(new FinalizouModel
                {
                    Name = itemFinalizou?.Name ?? string.Empty,
                    Health = itemFinalizou?.Health ?? 0f,
                    Distance = (itemFinalizou?.Distanece / 1000) ?? 0f,
                    DamageCauserName = itemFinalizou?.DamageCauserName,
                    DamageReason = itemFinalizou?.DamageReason,
                    DamageTypeCategory = itemFinalizou?.DamageTypeCategory
                });
            }
            #endregion
        }

        [RelayCommand]
        private async Task ExcluirDados()
        {
            var dados = await DatabaseDados.GetDadosJogadorAsync();
            foreach (var item in dados)
                await DatabaseDados.DeleteDadosJogadorAsync(item);

            var resultVictimDerrubou = await DatabaseDados.GetTelemetryVictimDerrubouAsync();
            foreach (var item in resultVictimDerrubou)
                await DatabaseDados.DeleteTelemetryVictimDerrubouAsync(item);

            var resultVictimFinalizou = await DatabaseDados.GetTelemetryVictimFinalizouAsync();
            foreach (var item in resultVictimFinalizou)
                await DatabaseDados.DeleteTelemetryVictimFinalizouAsync(item);

            var resultDerrubou = await DatabaseDados.GetTelemetryDerrubouAsync();
            foreach (var item in resultDerrubou)
                await DatabaseDados.DeleteTelemetryDerrubouAsync(item);

            var resultFinalizou = await DatabaseDados.GetTelemetryFinalizouAsync();
            foreach (var item in resultFinalizou)
                await DatabaseDados.DeleteTelemetryFinalizouAsync(item);

            var resultDataControle = await DatabaseDados.GetDataHoraControleAsync();
            foreach (var item in resultDataControle)
                await DatabaseDados.DeleleDataHoraControleAsync(item);

        }
    }
}
