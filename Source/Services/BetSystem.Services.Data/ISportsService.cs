namespace BetSystem.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using BetSystem.Data.Models;

    public interface ISportsService
    {
        IQueryable<Sport> GetAll();

        void AddOrUpdate(IEnumerable<Sport> sports);
    }
}
