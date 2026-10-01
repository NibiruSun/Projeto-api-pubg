using ApiPubg.Models.Painel;
using ApiPubg.Models.ServicesModel;
using ApiPubg.Popups;
using ApiPubg.Services.Databases;
using ApiPubg.ViewModel;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Extensions;
using Microsoft.Maui.Controls.Shapes;

namespace ApiPubg.Views;

//[QueryProperty(nameof(Nome), "nome")]
public partial class Painel : ContentPage
{
    private readonly PainelViewModel _viewModel;

    bool isPopupAbrir = false;
    public Painel(PainelViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        _viewModel = vm;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var conexao = await AbrirConexao();
        var jogador = await conexao.GetJogadorAsync();

        if (jogador.Count == 0)
        {
            AbrirPopup();
        }
        else
        {
            _viewModel.NickJogador = jogador.Select(j => j.Nome).FirstOrDefault() ?? string.Empty;

            var dados = await _viewModel.DatabaseDados.GetDadosJogadorAsync();

            if (dados == null) return;

            _viewModel.DadosJogadorPainel.Primeiro = dados.Count(d => d.WinPlace == 1);
            _viewModel.DadosJogadorPainel.Segundo = dados.Count(d => d.WinPlace == 2);
            _viewModel.DadosJogadorPainel.Terceiro = dados.Count(d => d.WinPlace == 3);
            _viewModel.DadosJogadorPainel.Quarto = dados.Count(d => d.WinPlace == 4);
            _viewModel.DadosJogadorPainel.Quinto = dados.Count(d => d.WinPlace == 5);
            _viewModel.DadosJogadorPainel.Sexto = dados.Count(d => d.WinPlace == 6);
            _viewModel.DadosJogadorPainel.Setimo = dados.Count(d => d.WinPlace == 7);
            _viewModel.DadosJogadorPainel.Oitavo = dados.Count(d => d.WinPlace == 8);
            _viewModel.DadosJogadorPainel.Nono = dados.Count(d => d.WinPlace == 9);
            _viewModel.DadosJogadorPainel.Decimo = dados.Count(d => d.WinPlace == 10);
            _viewModel.DadosJogadorPainel.QtdPartidas = dados.Count;
            _viewModel.DadosJogadorPainel.kills = dados.Sum(d => d.Kills);
            _viewModel.DadosJogadorPainel.Dbnos = dados.Sum(d => d.Dbnos);
            _viewModel.DadosJogadorPainel.Asssits = dados.Sum(d => d.Assists);
            _viewModel.DadosJogadorPainel.DamageDealt = dados.Sum(d => d.DamageDealt);
            _viewModel.DadosJogadorPainel.LongestKill = dados.Select(d => d.LongestKill).DefaultIfEmpty().Max();
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
            _viewModel.DadosJogadorPainel.TotalHora = Math.Round((totalMinutos / 60) / 60, 2);

            var databaseDados = await _viewModel.DatabaseDados.GetDadosJogadorAsync();

            foreach (var item in dados)
            {
                _viewModel.DadosJogador.Add(item);
            }
        }
    }
    private async void AbrirPopup()
    {
        if (isPopupAbrir) return;
        isPopupAbrir = true;

        var result = await this.ShowPopupAsync<NickPainel>
            (
                (View)new JogadorPopup() { BackgroundColor = Colors.Transparent, WidthRequest = 300, HeightRequest = 300 },
                new PopupOptions
                {
                    Shape = new RoundRectangle
                    {
                        Stroke = Colors.Transparent
                    }
                },

                CancellationToken.None
            );

        if (string.IsNullOrEmpty(result.Result?.Nome))
        {
            await Shell.Current.DisplayAlertAsync("Popup", "O nick não foi informando.", "OK");
            isPopupAbrir = false;
            AbrirPopup();
        }
        else
        {
            var conexao = await AbrirConexao();
            await conexao.AddJogadorAsync(new JogadorModel() { Nome = result.Result.Nome });
        }
    }
    private async Task<DatabaseDadosJogador> AbrirConexao()
    {
        var dpPath = System.IO.Path.Combine(FileSystem.AppDataDirectory, "DadosJogador");
        var dataBase = new DatabaseDadosJogador(dpPath);

        return dataBase;
    }

    private void GridItemsLayout_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {

    }
}