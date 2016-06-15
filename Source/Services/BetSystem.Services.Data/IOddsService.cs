namespace BetSystem.Services.Data
{
    using System.Collections.Generic;

    using BetSystem.Data.Models;
    using System.Linq;

    public interface IOddsService
    {
        void AddOrUpdate(IEnumerable<Odd> odds);

        IQueryable<Odd> GetAll();
    }
}
