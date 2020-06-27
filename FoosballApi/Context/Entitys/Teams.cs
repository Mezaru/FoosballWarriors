using System;
using System.Collections.Generic;

namespace FoosballApi.Context.Entitys
{
    public partial class Teams
    {
        public Teams()
        {
            PlayedMatchTeam1Navigation = new HashSet<PlayedMatch>();
            PlayedMatchTeam2Navigation = new HashSet<PlayedMatch>();
        }

        public int Id { get; set; }
        public int OffensePlayerId { get; set; }
        public int DefensePlayerId { get; set; }
        public string Name { get; set; }

        public virtual Player DefensePlayer { get; set; }
        public virtual Player OffensePlayer { get; set; }
        public virtual ICollection<PlayedMatch> PlayedMatchTeam1Navigation { get; set; }
        public virtual ICollection<PlayedMatch> PlayedMatchTeam2Navigation { get; set; }
    }
}
