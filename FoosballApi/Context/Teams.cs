using System;
using System.Collections.Generic;

namespace FoosballApi.Context
{
    public partial class Teams
    {
        public Teams()
        {
            PlayedMatchTeam1Navigation = new HashSet<PlayedMatch>();
            PlayedMatchTeam2Navigation = new HashSet<PlayedMatch>();
        }

        public int Id { get; set; }
        public int PlayerId1 { get; set; }
        public int PlayerId2 { get; set; }
        public string Name { get; set; }
        public int Rating { get; set; }

        public virtual Player PlayerId1Navigation { get; set; }
        public virtual Player PlayerId2Navigation { get; set; }
        public virtual ICollection<PlayedMatch> PlayedMatchTeam1Navigation { get; set; }
        public virtual ICollection<PlayedMatch> PlayedMatchTeam2Navigation { get; set; }
    }
}
