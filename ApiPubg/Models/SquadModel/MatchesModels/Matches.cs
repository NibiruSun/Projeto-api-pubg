using System;
using System.Collections.Generic;
using System.Text;

namespace ApiPubg.Models.SquadModel.MatchesModels
{
    public class Matches
    {
        public MatchesData? Data { get; set; }
        public List<MatchesIncluded>? Included { get; set; }
   }
    public class MatchesData
    {
        public string? Type { get; set; }
        public AttributesDataMatches? Attributes { get; set; }
        public MatchesDataRelationShips? RelationShips { get; set; }
    }
    public class AttributesDataMatches
    {
        public int Index { get; set; }
        private int _duration;
        public int Duration 
        {
            get => _duration;
            set
            {
                _duration = value;
                TempoMinuto = value / 60;
            }
        }
        public string? GameMode { get; set; }
        public string? MapName { get; set; }

        private string? _createdAt;
        public string? CreatedAt
        {
            get => _createdAt;
            set 
            {
              _createdAt = value;
                DataPartida = Convert.ToDateTime(value);
            }
        }
        public decimal TempoMinuto { get; set; }
        public DateTime DataPartida { get; set; }
        public int QuantidadeJogadores { get; set; }
    }
    public class MatchesDataRelationShips
    {
        public RelationShipsAssets? Assets { get; set; }
        public RelationShipsRosters? Rosters { get; set; }
    }

    public class RelationShipsAssets
    {
        public List<AssetsData>? Data { get; set; }
    }

    public class AssetsData
    {
        public string? Type { get; set; }
        public string? Id { get; set; }
    }
    public class RelationShipsRosters
    {
        public List<RostersData>? Data { get; set; }
    }
    public class RostersData
    {
        public string? Type { get; set; }
        public string? Id { get; set; }
    }


}
