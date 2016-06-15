namespace BetSystem.Services.Data
{
    using System;
    using System.Collections.Generic;

    using BetSystem.Data.Models;
    using BetSystem.Data.Common;
    using System.Linq;
    public class BetsService : IBetsService
    {
        private readonly IDbRepository<Bet> bets;

        public BetsService(IDbRepository<Bet> bets)
        {
            this.bets = bets;
        }

        public void AddOrUpdate(IEnumerable<Bet> bets)
        {
            var allBets = this.bets.All();

            foreach (var bet in bets)
            {
                if (allBets.Any(e => e.Key == bet.Key))
                {
                    var updatedBet = allBets.FirstOrDefault(s => s.Key == bet.Key);
                    updatedBet.Name = bet.Name;
                    updatedBet.IsLive = bet.IsLive;
                }
                else
                {
                    this.bets.Add(bet);
                }
            }

            this.bets.Save();
        }

        public IQueryable<Bet> GetAll()
        {
            return this.bets.All();
        }
    }
}
