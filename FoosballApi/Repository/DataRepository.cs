using FoosballApi.Context;
using FoosballApi.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoosballApi.Repository
{
    public class DataRepository : IDataRepository
    {
        private readonly FoosballContext context;

        public DataRepository(FoosballContext context)
        {
            this.context = context;
        }

        public Player GetPlayer(int id)
        {
            return context.Set<Player>().FirstOrDefault(x => x.Id == id);
        }

        public List<Player> GetAllPlayer()
        {
            return context.Set<Player>().ToList();
        }

    }
}
