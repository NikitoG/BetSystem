namespace BetSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BetSystem.Data.Common;
    using BetSystem.Data.Models;
    using BetSystem.Data.Common.Models;
    public class SportsService : ISportsService
    {
        private readonly IDbRepository<Sport> sports;

        public SportsService(IDbRepository<Sport> sports)
        {
            this.sports = sports;
        }

        public void AddOrUpdate(IEnumerable<Sport> sports)
        {
            var allSports = this.sports.All();

            foreach (var sport in sports)
            {
                if(allSports.Any(s => s.Key == sport.Key))
                {
                    var updatedSport = allSports.FirstOrDefault(s => s.Key == sport.Key);
                    updatedSport.Name = sport.Name;
                }
                else
                {
                    this.sports.Add(sport);
                }
            }

            this.sports.Save();
        }

        public IQueryable<Sport> GetAll()
        {
            return this.sports.All();
        }
    }
}
