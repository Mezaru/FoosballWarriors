using System;
using System.Collections.Generic;

namespace FoosballApi.Context.Entitys
{
    public partial class PlayerRating
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int? MatchId { get; set; }
        public int OffensiveRating { get; set; }
        public int DefensiveRating { get; set; }
    }
}
