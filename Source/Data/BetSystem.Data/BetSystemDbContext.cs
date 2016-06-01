namespace BetSystem.Data
{
    using System;
    using System.Data.Entity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;

    public class BetSyStemDbContext : IdentityDbContext<User>, IBetSystemDbContext
    {
        public BetSyStemDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public IDbSet<Bet> Bets { get; set; }

        public IDbSet<Event> Events { get; set; }

        public IDbSet<Match> Games { get; set; }

        public IDbSet<Odd> Odds { get; set; }

        public IDbSet<Sport> Sports { get; set; }

        public static BetSyStemDbContext Create()
        {
            return new BetSyStemDbContext();
        }
    }
}
