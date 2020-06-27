using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoosballApi.Models
{
    public class TeamModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public PlayerModel OffensPlayer { get; set; }
        public PlayerModel DefensePlayer { get; set; }
        public int Rating { get; set; }
    }
}
