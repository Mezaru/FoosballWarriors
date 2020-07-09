using FoosballApi.Models;

namespace FoosballApi.Interface
{
    public interface IMatchService
    {
        bool SaveNewMatch(MatchModel match);
    }
}