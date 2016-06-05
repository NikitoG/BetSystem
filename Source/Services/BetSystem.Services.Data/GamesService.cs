namespace BetSystem.Services.Data
{
    using System;
    using System.Linq;

    using BetSystem.Data.Models;
    using BetSystem.Data.Common;
    public class GamesService : IGamesSevice
    {
        private readonly IDbRepository<Match> games;

        public GamesService(IDbRepository<Match> games)
        {
            this.games = games;
        }

        public IQueryable<Match> GetAllMatchesBySportId(int id, bool all)
        {
            var currentDate = DateTime.Now;
            var nextDay = DateTime.Now.AddDays(1);

            return this.games
                .All()
                .Where(g => g.Event.SportId == id &&
                            g.StartDate > currentDate &&
                            all ? true : g.StartDate > nextDay)
                .OrderBy(g => g.EventId)
                .ThenBy(g => g.StartDate);
        }
    }
}
