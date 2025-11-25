using ApiPubg;
using ApiPubg.Models;
using ApiPubg.Models.SquadModel;
using ApiPubg.Models.SquadModel.MatchesModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiPubg.Models.SquadModel
{
    public class JogadorData
    {
        public string? Type { get; set; }
        public string? Id { get; set; }
        public Attributes? Attributes { get; set; }
        public Relationships? Relationships { get; set; }

    }
    public class Attributes
    {
        public string? Name { get; set; }
        public string? Stats { get; set; }
    }

    public class Relationships
    {
        public RelationshipsMatches? Matches { get; set; }
    }

    public class RelationshipsMatches
    {
        public List<MatchesData>? Data { get; set; }
    }

    public class MatchesData
    {
        public string? Id { get; set; }

    }

}
