using Data.Context;
using Data.Inteface;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repository
{
    public class DataRepository : IDataRepository
    {
        private readonly FoosballWarriorsEntities context;

        public DataRepository(FoosballWarriorsEntities context)
        {
            this.context = context;
        }

        public List<Player> GetPlayers()
        {
            return context.Set<Player>().ToList();
        }

        public Player GetPlayer(int id)
        {
            return context.Set<Player>().FirstOrDefault(x => x.Id == id);
        }
    }
}
