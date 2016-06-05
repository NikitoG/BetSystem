namespace BetSystem.Web.Api.Models.Games
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using Bets;
    using Data.Models;
    using Infrastructure.Mapping;

    public class AllGamesBySportResponceView : IMapFrom<Match>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public int Key { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public string Event { get; set; }

        public string EventName
        {
            get
            {
                return this.Event.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
            }
        }

        public string Group
        {
            get
            {
                return this.Event.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
            }
        }

        public int BetCount { get; set; }

        public BetResponceModel Bet { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Match, AllGamesBySportResponceView>()
                .ForMember(g => g.Event, opt => opt.MapFrom(e => e.Event.Name))
                .ForMember(g => g.BetCount, opt => opt.MapFrom(b => b.Bets.Count))
                .ForMember(g => g.Bet, opt => opt.MapFrom(g => g.Bets.FirstOrDefault()));
        }
    }
}