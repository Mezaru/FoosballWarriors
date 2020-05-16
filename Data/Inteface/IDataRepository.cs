using Data.Context;
using System.Collections.Generic;

namespace Data.Inteface
{
    public interface IDataRepository
    {
        Player GetPlayer(int id);
        List<Player> GetPlayers();
    }
}