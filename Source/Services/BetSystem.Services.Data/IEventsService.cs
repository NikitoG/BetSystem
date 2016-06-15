namespace BetSystem.Services.Data
{
    using BetSystem.Data.Models;
    using System.Collections.Generic;
    using System.Linq;

    public interface IEventsService
    {
        void AddOrUpdate(IEnumerable<Event> events);

        IQueryable<Event> GetAll();
    }
}
