using System;
using System.Collections.Generic;

namespace FoosballApi.Context.Entitys
{
    public partial class PlayedMatch
    {
        public int Id { get; set; }
        public int OffencePlayer1 { get; set; }
        public int DefencePlayer1 { get; set; }
        public int Team1 { get; set; }
        public int ScoreTeam1 { get; set; }
        public int OffencePlayer2 { get; set; }
        public int DefencePlayer2 { get; set; }
        public int Team2 { get; set; }
        public int ScoreTeam2 { get; set; }
        public DateTime MatchTime { get; set; }

        public virtual Player DefencePlayer1Navigation { get; set; }
        public virtual Player DefencePlayer2Navigation { get; set; }
        public virtual Player OffencePlayer1Navigation { get; set; }
        public virtual Player OffencePlayer2Navigation { get; set; }
        public virtual Teams Team1Navigation { get; set; }
        public virtual Teams Team2Navigation { get; set; }
    }
}
