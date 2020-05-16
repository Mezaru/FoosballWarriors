using Data.Context;
using Data.Inteface;
using System.Collections.Generic;

namespace Data.Services
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

        public List<Player> GetPlayers()
        {
            return repo.GetPlayers();
        }
    }
}
