using System;
using System.Collections.Generic;
using System.Text;

namespace ApiPubg.Models.SquadModel.MatchesModels
{
    public class MatchesIncluded
    {
        public string? Type { get; set; }
        public string? Id { get; set; }
        public AttributesIncludedMatches? Attributes { get; set; }
        public IncludesRelationShips? RelationShips { get; set; }
    }

    public class AttributesIncludedMatches
    {
        public StatsMatches? Stats { get; set; }
        public string? Won { get; set; }
        public string? ShardId { get; set; }
        public string? URL { get; set; }
    }

    public class StatsMatches
    {
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
