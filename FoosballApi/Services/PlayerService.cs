using FoosballApi.Context;
using FoosballApi.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoosballApi.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IDataRepository repo;

        public PlayerService(IDataRepository repo)
        {
            this.repo = repo;
        }

        public Player GetPlayer(int id)
        {
            return repo.GetPlayer(id);
        }

        public List<Player> GetAllPlayer()
        {
            return repo.GetAllPlayer();
        }
    }
}
