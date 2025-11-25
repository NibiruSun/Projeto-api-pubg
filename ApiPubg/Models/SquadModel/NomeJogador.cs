using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiPubg.Models.SquadModel
{
    public class NomeJogador : ObservableObject
    {
        public static string? NomeJogadorStatic;

        private string? _nome = "NibiruSun";
        public string? Nome
        { 
            get => _nome;
            set
            {
                SetProperty(ref _nome, value);
            }
        }

        private string? _nomeResultData ;
        public string? NomeResultData
        {
            get => _nomeResultData;
            set
            {
                SetProperty(ref _nomeResultData, value);
                NomeJogadorStatic = value;
            }
        }

    }
}
