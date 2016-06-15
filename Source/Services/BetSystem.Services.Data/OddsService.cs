namespace BetSystem.Services.Data
{
    using System;
    using System.Collections.Generic;

    using BetSystem.Data.Models;
    using BetSystem.Data.Common;
    using System.Linq;
    public class OddsService : IOddsService
    {
        private readonly IDbRepository<Odd> odds;

        public OddsService(IDbRepository<Odd> odds)
        {
            this.odds = odds;
        }

        public void AddOrUpdate(IEnumerable<Odd> odds)
        {
            var allOdds = this.odds.All();

            foreach (var odd in odds)
            {
                if (allOdds.Any(e => e.Key == odd.Key))
                {
                    var updatedOdds = allOdds.FirstOrDefault(s => s.Key == odd.Key);
                    updatedOdds.Name = odd.Name;
                    updatedOdds.SpecialBetValue = odd.SpecialBetValue;
                    updatedOdds.Value = odd.Value;
                }
                else
                {
                    this.odds.Add(odd);
                }
            }

            this.odds.Save();
        }

        public IQueryable<Odd> GetAll()
        {
            return this.odds.All();
        }
    }
}
