namespace BetSystem.Services.Data
{
    using System.Collections.Generic;

    using BetSystem.Data.Models;
    using System.Linq;
    public interface IBetsService
    {
        void AddOrUpdate(IEnumerable<Bet> bets);

        IQueryable<Bet> GetAll();
    }
}
