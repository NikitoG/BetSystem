namespace BetSystem.Web.Api.Models.Sports
{
    using System;
    using System.Linq;

    using AutoMapper;
    using BetSystem.Data.Models;
    using BetSystem.Web.Infrastructure.Mapping;
    public class EventsNameViewModel : IMapFrom<Event>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Key { get; set; }

        public string EventName
        {
            get
            {
                return this.Name.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
            }
        }

        public string Group
        {
            get
            {
                return this.Name.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
            }
        }

        public int FutureGames { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Event, EventsNameViewModel>()
                .ForMember(s => s.FutureGames, opt => opt.MapFrom(e => e.Games.Count(g => g.StartDate > DateTime.Now)));
        }
    }
}