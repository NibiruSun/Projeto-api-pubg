using System;
using System.Collections.Generic;
using System.Text;

namespace ApiPubg.Models.SquadModel
{
    public class SquadJogadores
    {
        public int Posicao { get; set; }
        public int Rank { get; set; }
        public int TeamId { get; set; }
        public int WinPlace { get; set; }
        public bool Ganhou { get; set; }
        public string? Id { get; set; }
        public int TotalKill { get; set; }
        public int TotalDerrubado { get; set; }
        public int TotalAssist { get; set; }
        public decimal TotalDano { get; set; }
        public List<Jogador>? Jogador { get; set; }
    }
    public class Jogador
    {
        public int index { get; set; }
        public string? NomeJogador { get; set; }
        public int Dbnos { get; set; }
        public int Assists { get; set; }
        public int boosts { get; set; }
        public decimal DamageDealt { get; set; }
        public string? DeathType { get; set; }
        public int HeadshotKills { get; set; }
        public int Heals { get; set; }
        public int KillPlace { get; set; }
        public int KillStreaks { get; set; }
        public int Kills { get; set; }
        public decimal LongestKill { get; set; }
        public string? PlayerId { get; set; }
        public int Revives { get; set; }
        public decimal RideDistance { get; set; }
        public int RoadKills { get; set; }
        public decimal SwimDistance { get; set; }
        public int TeamKills { get; set; }
        public decimal TimeSurvived { get; set; }
        public int VehicleDestroys { get; set; }
        public decimal WalkDistance { get; set; }
        public int WeaponsAcquired { get; set; }
        public int WinPlace { get; set; }
    }
}
