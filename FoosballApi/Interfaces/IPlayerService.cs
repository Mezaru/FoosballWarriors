using FoosballApi.Context;
using FoosballApi.Context.Entitys;
using FoosballApi.Models;
using System.Collections.Generic;

namespace FoosballApi.Interface
{
    public interface IPlayerService
    {
        List<PlayerModel> GetAllPlayer();
        PlayerModel GetPlayer(int id);
        int SaveNewPlayer(PlayerModel player);
        void SavePlayerImage(int playerId, string dbPath);
        bool DeletePlayer(int id);
        bool UppdatePlayer(Player player);
    }
}