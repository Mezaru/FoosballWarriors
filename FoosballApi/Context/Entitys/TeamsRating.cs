using System;
using System.Collections.Generic;

namespace FoosballApi.Context.Entitys
{
    public partial class TeamsRating
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public int? MatchId { get; set; }
        public int Rating { get; set; }
    }
}
