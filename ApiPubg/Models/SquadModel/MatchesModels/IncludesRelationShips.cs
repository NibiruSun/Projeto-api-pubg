using System;
using System.Collections.Generic;
using System.Text;

namespace ApiPubg.Models.SquadModel.MatchesModels
{
    public class IncludesRelationShips
    {
        public Participantes? Participants { get; set; }
    }

    public class Participantes
    {
        public List<ParticipantesData>? Data { get; set; }
    }
    public class ParticipantesData
    {
        public string? Type { get; set; }
        public string? Id { get; set; }
    }
}
