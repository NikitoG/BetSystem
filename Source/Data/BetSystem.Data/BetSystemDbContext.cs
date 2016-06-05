namespace BetSystem.Data
{
    using System;
    using System.Data.Entity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System.Linq;
    using Common.Models;
    public class BetSystemDbContext : IdentityDbContext<User>, IBetSystemDbContext
    {
        public BetSystemDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            try
            {
                return base.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ApplyAuditInfoRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (var entry in
                this.ChangeTracker.Entries()
                    .Where(
                        e =>
                        e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {
                var entity = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default(DateTime))
                {
                    entity.CreatedOn = DateTime.Now;
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }

        public IDbSet<Bet> Bets { get; set; }

        public IDbSet<Event> Events { get; set; }

        public IDbSet<Match> Games { get; set; }

        public IDbSet<Odd> Odds { get; set; }

        public IDbSet<Sport> Sports { get; set; }

        public static BetSystemDbContext Create()
        {
            return new BetSystemDbContext();
        }
    }
}
