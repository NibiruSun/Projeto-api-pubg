using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiPubg.Models.ServicesModel
{
    public class DadosJogadorModel
    {
        [PrimaryKey, AutoIncrement] // para SQLite-net
        public int Id { get; set; }
        public string? Data { get; set; }
        public string? Hora { get; set; }
        public DateTime? DH { get; set; }
        //[MaxLength(200)]
        public string? Telemetry { get; set; }
        //public int K { get; set; }
        //public int D { get; set; }
        //public int A { get; set; }
        //public int Win { get; set; }
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
        public string? Name { get; set; }
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
        public int Rank { get; set; }
        public int TeamId { get; set; }

    }
}
