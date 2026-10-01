using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiPubg.Models.ServicesModel
{
    public class DataHoraControle
    {
        [PrimaryKey, AutoIncrement] // para SQLite-net
        public int Id { get; set; }
        public DateTime? DH { get; set; }
    }
}
