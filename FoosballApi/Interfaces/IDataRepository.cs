using FoosballApi.Context;
using FoosballApi.Models;
using System.Collections.Generic;

namespace FoosballApi.Interface
{
    public interface IDataRepository
    {
        List<PlayerModel> GetAllPlayer();
        PlayerModel GetPlayer(int id);
        void SavePlayerImage(int playerId, string dbPath);
        int SaveNewPlayer(PlayerModel player);
    }
}