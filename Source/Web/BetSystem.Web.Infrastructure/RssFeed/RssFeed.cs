using BetSystem.Data.Models;
using BetSystem.Services.Data;
using System;
using System.Linq;
using System.Xml.Linq;

namespace BetSystem.Web.Infrastructure.RssFeed
{

    public class RssFeed
    {
        private readonly ISportsService sports;

        private readonly IEventsService events;

        private readonly IGamesSevice games;

        private readonly IBetsService bets;

        private readonly IOddsService odds;

        public RssFeed(ISportsService sports, IEventsService events, IGamesSevice games, IBetsService bets, IOddsService odds)
        {
            this.sports = sports;
            this.events = events;
            this.games = games;
            this.bets = bets;
            this.odds = odds;
        }

        public void UpdateData()
        {
            string RssFeedUrl = "http://vitalbet.net/sportxml ";
            try
            {
                XDocument xmlDoc = XDocument.Load(RssFeedUrl);

                var allSports =
                       from sport in xmlDoc.Descendants("Sport")
                       select new Sport
                       {
                           Name = sport.Attribute("Name").Value,
                           Key = int.Parse(sport.Attribute("ID").Value)
                       };

                this.sports.AddOrUpdate(allSports);

                var allEvents =
                       from competition in xmlDoc.Root.Descendants("Event")
                        let name = competition.Attribute("Name").Value
                        let key = int.Parse(competition.Attribute("ID").Value)
                        let category = int.Parse(competition.Attribute("CategoryID").Value.Trim())
                        let isLive = bool.Parse(competition.Attribute("IsLive").Value)
                        let parrent = int.Parse(competition.Parent.Attribute("ID").Value)
                        let sport = allSports.SingleOrDefault(s => s.Key == parrent)
                        select new Event
                        {
                            Name = name,
                            Key = key,
                            CategoryId = category,
                            IsLive = isLive,
                            Sport = sport
                        };

                this.events.AddOrUpdate(allEvents);

                var allGames =
                       from match in xmlDoc.Descendants("Match")
                       let name = match.Attribute("Name").Value
                       let key = int.Parse(match.Attribute("ID").Value)
                       let date = DateTime.Parse(match.Attribute("StartDate").Value)
                       let matchType = (MatchType)Enum.Parse(typeof(MatchType), match.Attribute("MatchType").Value, true)
                       let parrent = int.Parse(match.Parent.Attribute("ID").Value)
                       let ev = allEvents.SingleOrDefault(s => s.Key == parrent)
                       select new Match
                       {
                           Name = name,
                           Key = key,
                           StartDate = date,
                           MatchType = matchType,
                           Event = ev
                       };

                this.games.AddOrUpdate(allGames);

                var allBets =
                           from bet in xmlDoc.Descendants("Bet")
                           let name = bet.Attribute("Name").Value
                           let key = int.Parse(bet.Attribute("ID").Value)
                           let live = bool.Parse(bet.Attribute("IsLive").Value)
                           let parrent = int.Parse(bet.Parent.Attribute("ID").Value)
                           let match = allGames.SingleOrDefault(s => s.Key == parrent)
                           select new Bet
                           {
                               Name = name,
                               Key = key,
                               IsLive = live,
                               Match = match
                           };

                this.bets.AddOrUpdate(allBets);

                var allOdds =
                           from odd in xmlDoc.Descendants("Odd")
                           let name = odd.Attribute("Name").Value
                           let key = int.Parse(odd.Attribute("ID").Value)
                           let value = decimal.Parse(odd.Attribute("Value").Value)
                           let specialValue = odd.Attribute("SpecialBetValue")
                           let specialBetValue = specialValue != null ? decimal.Parse(specialValue.Value) : 0
                           let parrent = int.Parse(odd.Parent.Attribute("ID").Value)
                           let bet = allBets.SingleOrDefault(s => s.Key == parrent)
                           select new Odd
                           {
                               Name = name,
                               Key = key,
                               Value = value,
                               SpecialBetValue = specialBetValue,
                               Bet = bet
                           };

                this.odds.AddOrUpdate(allOdds);
            }
            catch (Exception ex)
            {
                throw;
            }            
        }
    }
}
