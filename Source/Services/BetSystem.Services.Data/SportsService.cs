namespace BetSystem.Services.Data
{
    using System;
    using System.Linq;
    using BetSystem.Data.Common;
    using BetSystem.Data.Models;
    using BetSystem.Services.Data.Contracts;

    public class SportsService : ISportsService
    {
        private readonly IDbRepository<Sport> sports;

        public SportsService(IDbRepository<Sport> sports)
        {
            this.sports = sports;
        }

        public IQueryable<Sport> GetAllSports(int page = 1)
        {
            return null;
        }
    }
}
