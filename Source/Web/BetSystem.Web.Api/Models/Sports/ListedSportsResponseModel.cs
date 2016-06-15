namespace BetSystem.Web.Api.Models.Sports
{
    using System.Linq;

    using AutoMapper;
    using BetSystem.Web.Infrastructure.Mapping;
    using Data.Models;
    using System.Collections.Generic;
    using System;
    public class ListedSportsResponseModel : IMapFrom<Sport>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Key { get; set; }

        public string NumberOfGames { get; set; }

        public IEnumerable<EventsNameViewModel> Events { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Sport, ListedSportsResponseModel>()
                .ForMember(s => s.NumberOfGames, opt => opt.MapFrom(s => s.Events.Sum(m => m.Games.Count(g => g.StartDate > DateTime.Now))));
        }
    }
}