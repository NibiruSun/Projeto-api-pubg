using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
namespace ApiPubg.Models.ServicesModel
{
    [Table("JogadorFinalizou")]

    public class TelemetryFinalizou
    {
        [PrimaryKey, AutoIncrement] // para SQLite-net
        public int Id { get; set; }
        public int IdDadosJogador { get; set; }
        public string? Name { get; set; }
        public float Health { get; set; }
        public string? DamageReason { get; set; }
        public string? DamageTypeCategory { get; set; }
        public string? DamageCauserName { get; set; }

        //public List<string>? AdditionalInfor { get; set; }
        public float Distanece { get; set; }
        public string? Data { get; set; }
        public string? Hora { get; set; }

    }
}
