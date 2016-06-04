namespace BetSystem.Web.Api.App_Start
{
    using System.Data.Entity;

    using BetSystem.Data;
    using BetSystem.Data.Migrations;

    public static class DatabaseConfig
    {
        public static void Initialize()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BetSystemDbContext, Configuration>());
        }
    }
}