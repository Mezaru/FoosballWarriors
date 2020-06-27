using FoosballApi.Context;
using FoosballApi.Context.Entitys;
using FoosballApi.Interface;
using FoosballApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

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
            var ratings = (from pr in context.Set<PlayerRating>()
                           join m in context.Set<PlayedMatch>() on pr.MatchId equals m.Id
                           where pr.PlayerId == id
                           select new { pr, lastPlayed = m.MatchTime }).TakeLast(1);

            return (from p in context.Player
                    join r in ratings on p.Id equals r.pr.PlayerId into rTemp
                    from r in rTemp.DefaultIfEmpty()
                    select new PlayerModel
                    {
                        Id = p.Id,
                        Name = p.Name,
                        ImageUrl = p.ImageUrl,
                        OffensiveRating = r.pr.OffensiveRating == null ? 1500 : r.pr.OffensiveRating,
                        DefensiveRating = r.pr.OffensiveRating == null ? 1500 : r.pr.OffensiveRating,
                        LastPlayed = r.lastPlayed
                    }).FirstOrDefault(x => x.Id == id);
        }

        public List<PlayerModel> GetAllPlayer()
        {
            var ratings = (from pr in context.Set<PlayerRating>()
                           join m in context.Set<PlayedMatch>() on pr.MatchId equals m.Id
                           orderby m.MatchTime descending
                           select new { pr, lastPlayed = m.MatchTime }).Take(1);

            return (from p in context.Player
                    join r in ratings on p.Id equals r.pr.PlayerId into rTemp
                    from r in rTemp.DefaultIfEmpty()
                    select new PlayerModel
                    {
                        Id = p.Id,
                        Name = p.Name,
                        ImageUrl = p.ImageUrl,
                        OffensiveRating = r.pr.OffensiveRating == null ? 1500 : r.pr.OffensiveRating,
                        DefensiveRating = r.pr.OffensiveRating == null ? 1500 : r.pr.OffensiveRating,
                        LastPlayed = r.lastPlayed
                    }).ToList();
        }

        public int SaveNewPlayer(PlayerModel player)
        {
            var newPlayer = new Player()
            {
                Name = player.Name,
            };
            context.Add(newPlayer);
            context.SaveChanges();
            context.Add(new PlayerRating()
            {
                OffensiveRating = 1500,
                DefensiveRating = 1500,
                PlayerId = newPlayer.Id
            });
            context.SaveChanges();
            return newPlayer.Id;
        }

        public void SavePlayerImage(int playerId, string dbPath)
        {
            var player = context.Find<Player>(playerId);
            player.ImageUrl = dbPath;

            context.SaveChanges();
        }

        public bool DeletePlayer(int id)
        {
            var player = context.Find<Player>(id);
            context.Remove(player);
            context.SaveChanges();
            return true;
        }

        public bool UppdatePlayer(Player player)
        {
            var editPlayer = context.Find<Player>(player.Id);
            editPlayer.Name = player.Name;

            context.SaveChanges();
            return true;
        }
    }
}
