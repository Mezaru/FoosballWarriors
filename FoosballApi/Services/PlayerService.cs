using FoosballApi.Context;
using FoosballApi.Context.Entitys;
using FoosballApi.Interface;
using FoosballApi.Models;
using System.Collections.Generic;

namespace FoosballApi.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IDataRepository repo;

        public PlayerService(IDataRepository repo)
        {
            this.repo = repo;
        }

        public PlayerModel GetPlayer(int id)
        {
            return repo.GetPlayer(id);
        }

        public List<PlayerModel> GetAllPlayer()
        {
            return repo.GetAllPlayer();
        }

        public int SaveNewPlayer(PlayerModel player)
        {
            return repo.SaveNewPlayer(player);
        }

        public void SavePlayerImage(int playerId, string dbPath)
        {
            repo.SavePlayerImage(playerId, dbPath);
        }

        public bool DeletePlayer(int id)
        {
            return repo.DeletePlayer(id);
        }

        public bool UppdatePlayer(Player player)
        {
            return repo.UppdatePlayer(player);
        }
    }
}
