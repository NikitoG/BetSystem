namespace BetSystem.Services.Data.Contracts
{
    using BetSystem.Data.Models;
    using System.Linq;

    public interface ISportsService
    {
        IQueryable<Sport> GetAllSports(int page = 1);
    }
}
