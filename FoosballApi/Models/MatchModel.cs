using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoosballApi.Models
{
    public class MatchModel
    {
        public PlayerModel OffencePlayer1 { get; set; }
        public PlayerModel OffencePlayer2 { get; set; }
        public PlayerModel DefencePlayer1 { get; set; }
        public PlayerModel DefencePlayer2 { get; set; }
        public int ScoreTeam1 { get; set; }
        public int ScoreTeam2 { get; set; }
    }
}
