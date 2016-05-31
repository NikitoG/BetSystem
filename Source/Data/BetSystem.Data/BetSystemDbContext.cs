namespace BetSystem.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;

    public class BetSyStemDbContext : IdentityDbContext<User>
    {
        public BetSyStemDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static BetSyStemDbContext Create()
        {
            return new BetSyStemDbContext();
        }
    }
}
