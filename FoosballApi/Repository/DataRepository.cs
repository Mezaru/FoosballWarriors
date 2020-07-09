using EloCalculator;
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

        public bool SaveNewMatch(MatchModel match)
        {
            var teamOne = context.Set<Teams>().FirstOrDefault(x => x.OffensePlayerId == match.OffencePlayer1.Id && x.DefensePlayerId == match.DefencePlayer1.Id);
            if (teamOne == null)
            {
                teamOne = new Teams
                {
                    OffensePlayerId = match.OffencePlayer1.Id,
                    DefensePlayerId = match.DefencePlayer1.Id,
                    Name = $"{match.OffencePlayer1.Name}_{match.DefencePlayer1.Name}"
                };
                context.Add(teamOne);
            }

            var teamTwo = context.Set<Teams>().FirstOrDefault(x => x.OffensePlayerId == match.OffencePlayer2.Id && x.DefensePlayerId == match.DefencePlayer2.Id);
            if (teamTwo == null)
            {
                teamTwo = new Teams
                {
                    OffensePlayerId = match.OffencePlayer2.Id,
                    DefensePlayerId = match.DefencePlayer2.Id,
                    Name = $"{match.OffencePlayer2.Name}_{match.DefencePlayer2.Name}"
                };
                context.Add(teamTwo);
            }

            var eloTeam1 = new EloTeam(match.ScoreTeam1 > match.ScoreTeam2);
            eloTeam1.AddPlayer(new EloPlayer(match.OffencePlayer1.OffensiveRating));
            eloTeam1.AddPlayer(new EloPlayer(match.DefencePlayer1.DefensiveRating));
            var eloTeam2 = new EloTeam(match.ScoreTeam2 > match.ScoreTeam1);
            eloTeam2.AddPlayer(new EloPlayer(match.OffencePlayer2.OffensiveRating));
            eloTeam2.AddPlayer(new EloPlayer(match.DefencePlayer2.DefensiveRating));
            var eloMatch = new EloMatch(new[] { eloTeam1, eloTeam2 });
            var result = eloMatch.Calculate();

            var team1Result = result.GetResults(eloTeam1.Identifier).ToList();
            var team2Result = result.GetResults(eloTeam2.Identifier);

            match.OffencePlayer1.OffensiveRating = team1Result.First().RatingAfter;
            match.DefencePlayer1.DefensiveRating = team1Result.Last().RatingAfter;
            match.OffencePlayer2.OffensiveRating = team2Result.First().RatingAfter;
            match.DefencePlayer2.DefensiveRating = team2Result.Last().RatingAfter;

            context.Add(new PlayedMatch
            {
                OffencePlayer1 = match.OffencePlayer1.Id,
                DefencePlayer1 = match.DefencePlayer1.Id,
                ScoreTeam1 = match.ScoreTeam1,
                OffencePlayer2 = match.OffencePlayer2.Id,
                DefencePlayer2 = match.OffencePlayer2.Id,
                ScoreTeam2 = match.ScoreTeam2,
                MatchTime = DateTime.Now,

            });

            return true;
        }
    }
}
