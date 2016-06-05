namespace BetSystem.Services.Data
{
    using System.Linq;

    using BetSystem.Data.Models;

    public interface IGamesSevice
    {
        IQueryable<Match> GetAllMatchesBySportId(int id, bool all);
    }
}
