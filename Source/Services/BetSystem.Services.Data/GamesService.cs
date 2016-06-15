namespace BetSystem.Services.Data
{
    using System;
    using System.Linq;

    using BetSystem.Data.Models;
    using BetSystem.Data.Common;
    using System.Collections.Generic;
    public class GamesService : IGamesSevice
    {
        private readonly IDbRepository<Match> games;

        public GamesService(IDbRepository<Match> games)
        {
            this.games = games;
        }

        public IQueryable<Match> GetAllMatchesBySport(string name, bool all)
        {
            var currentDate = DateTime.Now;
            var nextDay = DateTime.Now.AddHours(24);

            return this.games
                .All()
                .Where(g => g.Event.Sport.Name == name)
                .Where(g => g.StartDate > currentDate)
                .Where(g => all ? true : g.StartDate < nextDay)
                .OrderBy(g => g.EventId)
                .ThenBy(g => g.StartDate);
        }

        public IQueryable<Match> GetAllMatchesByEvent(int eventKey, bool all)
        {
            var currentDate = DateTime.Now;
            var nextDay = DateTime.Now.AddHours(24);

            return this.games
                .All()
                .Where(g => g.Event.Key == eventKey)
                .Where(g => g.StartDate > currentDate)
                .Where(g => all ? true : g.StartDate < nextDay)
                .OrderBy(g => g.StartDate);
        }

        public IQueryable<Match> GetDetails(int key)
        {
            return this.games
                .All()
                .Where(g => g.Key == key);
        }

        public void AddOrUpdate(IEnumerable<Match> games)
        {
            var allGames = this.games.All();

            foreach (var match in games)
            {
                if (allGames.Any(e => e.Key == match.Key))
                {
                    var updatedMatch = allGames.FirstOrDefault(s => s.Key == match.Key);
                    updatedMatch.Name = match.Name;
                    updatedMatch.StartDate = match.StartDate;
                }
                else
                {
                    this.games.Add(match);
                }
            }

            this.games.Save();
        }

        public IQueryable<Match> GetAll()
        {
            return this.games.All();
        }
    }
}
