namespace BetSystem.Services.Data
{
    using System;
    using System.Linq;
    using BetSystem.Data.Common;
    using BetSystem.Data.Models;

    public class SportsService : ISportsService
    {
        private readonly IDbRepository<Sport> sports;

        public SportsService(IDbRepository<Sport> sports)
        {
            this.sports = sports;
        }

        public IQueryable<Sport> GetAllSports()
        {
            return this.sports.All();
        }
    }
}
