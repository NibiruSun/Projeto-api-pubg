using ApiPubg.Models.SquadModel.MatchesModels;
using ApiPubg.Services.Databases;
using ApiPubg.ViewModel;
using CommunityToolkit.Maui.Extensions;
using System.IO;

namespace ApiPubg.Views;

public partial class BuscarJogosView : ContentPage
{
    private readonly ConnectionApiPubg _viewModel;
    public BuscarJogosView(ConnectionApiPubg vm)
    {
        InitializeComponent();

        BindingContext = vm;
        _viewModel = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var dpPath = Path.Combine(FileSystem.AppDataDirectory, "DadosJogado");
        DatabaseDadosJogador conexao= new DatabaseDadosJogador(dpPath);

        var jogador = await conexao.GetJogadorAsync();

        _viewModel.NomeJogador.Nome = jogador.Select(j => j.Nome).FirstOrDefault();
    }
}

