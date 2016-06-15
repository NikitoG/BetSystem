namespace RssCrawler
{
    using BetSystem.Data;
    using BetSystem.Data.Common;
    using BetSystem.Data.Common.Models;
    using BetSystem.Data.Models;
    using BetSystem.Services.Data;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Timers;
    using System.Xml.Linq;

    public class RssFeed
    {
        private static Timer aTimer;
        private static List<Sport> oldSports = new List<Sport>();
        private static List<Sport> newSports = new List<Sport>();
        private static BetSystemDbContext db = new BetSystemDbContext();
        private static DbRepository<Sport> repoSports = new DbRepository<Sport>(db);
        private static DbRepository<Event> repoEvents = new DbRepository<Event>(db);
        private static DbRepository<Match> repoGames = new DbRepository<Match>(db);
        private static DbRepository<Bet> repoBets = new DbRepository<Bet>(db);
        private static DbRepository<Odd> repoOdds = new DbRepository<Odd>(db);
        private static SportsService sports = new SportsService(repoSports);
        private static EventsService events = new EventsService(repoEvents);
        private static GamesService games = new GamesService(repoGames);
        private static BetsService bets = new BetsService(repoBets);
        private static OddsService odds = new OddsService(repoOdds);

        public static void Start()
        {

            // Create a timer with a ten second interval.
            aTimer = new System.Timers.Timer();

            // Hook up the Elapsed event for the timer.
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);

            // Set the Interval to 2 seconds (2000 milliseconds).
            aTimer.Interval = 60000;
            aTimer.Enabled = true;

            Console.WriteLine("Press the Enter key to exit the program.");
            Console.ReadLine();

            // If the timer is declared in a long-running method, use 
            // KeepAlive to prevent garbage collection from occurring 
            // before the method ends. 
            //GC.KeepAlive(aTimer);

        }

        // Specify what you want to happen when the Elapsed event is  
        // raised. 
        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            Console.WriteLine("Start new!");
            GetData();
        }

        public static void GetData()
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

                var oldSports = sports.GetAll();
                var newSports = allSports.Except(oldSports, new SportsComparer()).ToList();

                sports.AddOrUpdate(newSports);

                allSports = sports.GetAll().ToList();

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

                var oldEvents = events.GetAll();
                var newEvents = allEvents.Except(oldEvents, new EventsComparer()).ToList();

                events.AddOrUpdate(newEvents);

                allEvents = events.GetAll().ToList();

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

                var oldGames = games.GetAll();
                var newGames = allGames.Except(oldGames, new GamesComparer()).ToList();

                games.AddOrUpdate(newGames);

                allGames = games.GetAll().ToList();

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

                var oldBets = bets.GetAll();
                var newBets = allBets.Except(oldBets, new BetsComparer()).ToList();

                bets.AddOrUpdate(newBets);

                allBets = bets.GetAll().ToList();

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

                var oldOdds = odds.GetAll();
                var newOdds = allOdds.Except(oldOdds, new OddsComparer()).ToList();

                odds.AddOrUpdate(newOdds);
                Console.WriteLine("Finish");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
