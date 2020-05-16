using FoosballApi.Context;
using System.Collections.Generic;

namespace FoosballApi.Interface
{
    public interface IPlayerService
    {
        List<Player> GetAllPlayer();
        Player GetPlayer(int id);
    }
}