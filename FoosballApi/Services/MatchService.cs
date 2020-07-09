using FoosballApi.Context;
using FoosballApi.Context.Entitys;
using FoosballApi.Interface;
using FoosballApi.Models;
using System.Collections.Generic;

namespace FoosballApi.Services
{
    public class MatchService : IMatchService
    {
        private readonly IDataRepository repo;

        public MatchService(IDataRepository repo)
        {
            this.repo = repo;
        }

        public bool SaveNewMatch(MatchModel match)
        {
            return repo.SaveNewMatch(match);
        }
    }
}
