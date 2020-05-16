using FoosballApi.Context;
using System.Collections.Generic;

namespace FoosballApi.Interface
{
    public interface IDataRepository
    {
        List<Player> GetAllPlayer();
        Player GetPlayer(int id);
    }
}