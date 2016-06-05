namespace BetSystem.Data.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.ServiceModel.Syndication;
    using System.Xml;
    using System.Xml.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<BetSystemDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(BetSystemDbContext context)
        {
            string RssFeedUrl = "http://vitalbet.net/sportxml ";
            try
            {
                XDocument xmlDoc = XDocument.Load(RssFeedUrl);

                if (!context.Sports.Any())
                {
                    var sports =
                       from sport in xmlDoc.Descendants("Sport")
                       select new Sport
                       {
                           Name = sport.Attribute("Name").Value,
                           Key = int.Parse(sport.Attribute("ID").Value)
                       };
                    foreach (var sport in sports)
                    {
                        context.Sports.Add(sport);
                    }
                    context.SaveChanges();
                }

                if (!context.Events.Any())
                {
                    var events =
                       (from competition in xmlDoc.Root.Descendants("Event")
                        let name = competition.Attribute("Name").Value
                        let key = int.Parse(competition.Attribute("ID").Value)
                        let category = int.Parse(competition.Attribute("CategoryID").Value.Trim())
                        let isLive = bool.Parse(competition.Attribute("IsLive").Value)
                        let parrent = int.Parse(competition.Parent.Attribute("ID").Value)
                        select new Event
                        {
                            Name = name,
                            Key = key,
                            CategoryId = category,
                            IsLive = isLive,
                            Sport = context.Sports.SingleOrDefault(s => s.Key == parrent)
                        }).ToArray();
                    foreach (var competition in events)
                    {
                        if(competition.Sport != null)
                        {
                            context.Events.Add(competition);
                        }
                    }
                    context.SaveChanges();
                }

                if (!context.Games.Any())
                {
                    try
                    {
                        var games =
                       from match in xmlDoc.Descendants("Match")
                       let parrent = int.Parse(match.Parent.Attribute("ID").Value)
                       select new Match
                       {
                           Name = match.Attribute("Name").Value,
                           Key = int.Parse(match.Attribute("ID").Value),
                           StartDate = DateTime.Parse(match.Attribute("StartDate").Value),
                           MatchType = (MatchType)Enum.Parse(typeof(MatchType), match.Attribute("MatchType").Value, true),
                           Event = context.Events.SingleOrDefault(s => s.Key == parrent)
                       };
                        foreach (var match in games)
                        {
                            if(match.Event != null)
                            {
                                context.Games.Add(match);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                    }
                    context.SaveChanges();
                }

                if (!context.Bets.Any())
                {
                    try
                    {
                        var bets =
                           from bet in xmlDoc.Descendants("Bet")
                           let parrent = int.Parse(bet.Parent.Attribute("ID").Value)
                           select new Bet
                           {
                               Name = bet.Attribute("Name").Value,
                               Key = int.Parse(bet.Attribute("ID").Value),
                               IsLive = bool.Parse(bet.Attribute("IsLive").Value),
                               Match = context.Games.SingleOrDefault(s => s.Key == parrent)
                           };
                        foreach (var bet in bets)
                        {
                            if(bet.Match != null)
                            {
                                context.Bets.Add(bet);
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                    context.SaveChanges();
                }

                if (!context.Odds.Any())
                {
                    try
                    {
                        var odds =
                           from odd in xmlDoc.Descendants("Odd")
                           let parrent = int.Parse(odd.Parent.Attribute("ID").Value)
                           let specialValue = odd.Attribute("SpecialBetValue")
                           select new Odd
                           {
                               Name = odd.Attribute("Name").Value,
                               Key = int.Parse(odd.Attribute("ID").Value),
                               Value = decimal.Parse(odd.Attribute("Value").Value),
                               SpecialBetValue = specialValue != null ?  decimal.Parse(specialValue.Value) : 0,
                               Bet = context.Bets.SingleOrDefault(s => s.Key == parrent)
                           };
                        foreach (var odd in odds)
                        {
                            if(odd.Bet != null)
                            {
                                context.Odds.Add(odd);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
