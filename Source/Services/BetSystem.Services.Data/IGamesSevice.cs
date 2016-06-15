namespace BetSystem.Services.Data
{
    using System.Linq;

    using BetSystem.Data.Models;
    using System.Collections.Generic;

    public interface IGamesSevice
    {
        IQueryable<Match> GetAllMatchesBySport(string name, bool all);

        IQueryable<Match> GetAllMatchesByEvent(int eventKey, bool all);

        IQueryable<Match> GetDetails(int key);

        void AddOrUpdate(IEnumerable<Match> games);

        IQueryable<Match> GetAll();
    }
}
