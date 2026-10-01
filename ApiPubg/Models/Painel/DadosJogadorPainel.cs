using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiPubg.Models.Painel
{
    public partial class DadosJogadorPainel : ObservableObject
    {
        private int _kill = 0;
        public int kills
        {
            get => _kill;
            set => SetProperty(ref _kill, value);
        }
        private int _dbnos = 0;
        public int Dbnos
        {
            get => _dbnos;
            set => SetProperty(ref _dbnos, value);
        }
        private int _asssits = 0;
        public int Asssits
        {
            get => _asssits;
            set => SetProperty(ref _asssits, value);
        }
        private decimal _damageDealt = 0;
        public decimal DamageDealt
        {
            get => _damageDealt;
            set => SetProperty(ref _damageDealt, value);
        }

        private int _qtdPartidas = 0;
        public int QtdPartidas
        {
            get => _qtdPartidas;
            set => SetProperty(ref _qtdPartidas, value);
        }

        private decimal _primeiro = 0;
        public decimal Primeiro
        {
            get => _primeiro;
            set => SetProperty(ref _primeiro, value);
        }

        private decimal _segundo = 0;
        public decimal Segundo
        {
            get => _segundo;
            set => SetProperty(ref _segundo, value);
        }

        private decimal _terceiro = 0;
        public decimal Terceiro
        {
            get => _terceiro;
            set => SetProperty(ref _terceiro, value);
        }

        private decimal _quarto = 0;
        public decimal Quarto
        {
            get => _quarto;
            set => SetProperty(ref _quarto, value);
        }

        private decimal _quinto = 0;
        public decimal Quinto
        {
            get => _quinto;
            set => SetProperty(ref _quinto, value);
        }

        private decimal _sexto = 0;
        public decimal Sexto
        {
            get => _sexto;
            set => SetProperty(ref _sexto, value);
        }

        private decimal _setimo = 0;
        public decimal Setimo
        {
            get => _setimo;
            set => SetProperty(ref _sexto, value);
        }

        private decimal _oitavo = 0;
        public decimal Oitavo
        {
            get => _oitavo;
            set => SetProperty(ref _oitavo, value);
        }

        private decimal _nono = 0;
        public decimal Nono
        {
            get => _nono;
            set => SetProperty(ref _nono, value);
        }

        private decimal _decimo = 0;
        public decimal Decimo
        {
            get => _decimo;
            set => SetProperty(ref _decimo, value);
        }

        private decimal _longestKill = 0;
        public decimal LongestKill
        {
            get => _longestKill;
            set => SetProperty(ref _longestKill, value);
        }

        private decimal _totalHora = 0;
        public decimal TotalHora
        {
            get => _totalHora;
            set => SetProperty(ref _totalHora, value);
        }

    }
}
