using System;
using System.Collections.Generic;

namespace FoosballApi.Context
{
    public partial class Player
    {
        public Player()
        {
            PlayedMatchDefencePlayer1Navigation = new HashSet<PlayedMatch>();
            PlayedMatchDefencePlayer2Navigation = new HashSet<PlayedMatch>();
            PlayedMatchOffencePlayer1Navigation = new HashSet<PlayedMatch>();
            PlayedMatchOffencePlayer2Navigation = new HashSet<PlayedMatch>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Rating { get; set; }
        public string ImageUrl { get; set; }

        public virtual Teams TeamsPlayerId1Navigation { get; set; }
        public virtual Teams TeamsPlayerId2Navigation { get; set; }
        public virtual ICollection<PlayedMatch> PlayedMatchDefencePlayer1Navigation { get; set; }
        public virtual ICollection<PlayedMatch> PlayedMatchDefencePlayer2Navigation { get; set; }
        public virtual ICollection<PlayedMatch> PlayedMatchOffencePlayer1Navigation { get; set; }
        public virtual ICollection<PlayedMatch> PlayedMatchOffencePlayer2Navigation { get; set; }
    }
}
