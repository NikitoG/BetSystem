namespace BetSystem.Web.Api.Models.Statistics
{
    using System;
    using System.Linq;
    using AutoMapper;
    using BetSystem.Data.Models;
    using BetSystem.Web.Infrastructure.Mapping;

    public class StatsViewModel : IMapFrom<Sport>, IHaveCustomMappings
    {
        public int Sports { get; set; }

        public int Events { get; set; }

        public int Games { get; set; }

        public int Bets { get; set; }

        public int Odds { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Sport, StatsViewModel>()
                .ForMember(s => s.Events, opt => opt.MapFrom(s => s.Events.Count))
                .ForMember(s => s.Games, opt => opt.MapFrom(s => s.Events.Sum(e => e.Games.Count)))
                .ForMember(s => s.Bets, opt => opt.MapFrom(s => s.Events.Sum(e => e.Games.Sum( g => g.Bets.Count))))
                .ForMember(s => s.Odds, opt => opt.MapFrom(s => s.Events.Sum(e => e.Games.Sum(g => g.Bets.Sum(b => b.Odds.Count)))));
        }
    }
}