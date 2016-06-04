namespace BetSystem.Services.Data
{
    using BetSystem.Data.Models;
    using System.Linq;

    public interface ISportsService
    {
        IQueryable<Sport> GetAllSports();
    }
}
