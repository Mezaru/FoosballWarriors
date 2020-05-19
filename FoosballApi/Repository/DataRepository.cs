using FoosballApi.Context;
using FoosballApi.Interface;
using FoosballApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace FoosballApi.Repository
{
    public class DataRepository : IDataRepository
    {
        private readonly FoosballContext context;
        public DataRepository(FoosballContext context)
        {
            this.context = context;
        }

        public PlayerModel GetPlayer(int id)
        {
            var player = context.Set<Player>().FirstOrDefault(x => x.Id == id);
            return new PlayerModel
            {
                Name = player.Name,
                Rating = player.Rating,
                ImageUrl = player.ImageUrl
            };
        }

        public List<PlayerModel> GetAllPlayer()
        {
            return context.Set<Player>().Select(x => new PlayerModel
            {
                Name = x.Name,
                Rating = x.Rating,
                ImageUrl = x.ImageUrl
            }).ToList();
        }

        public int SaveNewPlayer(PlayerModel player)
        {
            var newPlayer = new Player()
            {
                Name = player.Name
            };
            context.Add(newPlayer);
            context.SaveChanges();
            return newPlayer.Id;
        }

        public void SavePlayerImage(int playerId, string dbPath)
        {
            var player = context.Find<Player>(playerId);
            player.ImageUrl = dbPath;

            context.SaveChanges();
        }
    }
}
