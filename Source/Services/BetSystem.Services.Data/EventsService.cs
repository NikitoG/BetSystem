namespace BetSystem.Services.Data
{
    using System;
    using System.Collections.Generic;

    using BetSystem.Data.Models;
    using BetSystem.Data.Common;
    using System.Linq;
    using BetSystem.Data.Common.Models;
    public class EventsService : IEventsService
    {
        private readonly IDbRepository<Event> events;

        public EventsService(IDbRepository<Event> events)
        {
            this.events = events;
        }

        public IQueryable<Event> GetAll()
        {
            return this.events.All();
        }

        public void AddOrUpdate(IEnumerable<Event> events)
        {
            var allEvents = this.events.All();

            foreach (var currentEvent in events)
            {
                if (allEvents.Any(e => e.Key == currentEvent.Key))
                {
                    var updatedEvent = allEvents.FirstOrDefault(s => s.Key == currentEvent.Key);
                    updatedEvent.Name = currentEvent.Name;
                    updatedEvent.IsLive = currentEvent.IsLive;
                }
                else
                {
                    this.events.Add(currentEvent);
                }
            }

            this.events.Save();
        }
    }
}
