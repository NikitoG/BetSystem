namespace BetSystem.Web.Api.Models.Bets
{
    using System;
    using AutoMapper;
    using BetSystem.Web.Infrastructure.Mapping;
    using Data.Models;
    using System.Collections.Generic;
    using Odds;
    using System.Linq;
    public class BetResponceModel : IMapFrom<Bet>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public int Key { get; set; }

        public string Name { get; set; }

        public bool IsLive { get; set; }

        public string MatchName { get; set; }

        public ICollection<OddResponseModel> Odds { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Bet, BetResponceModel>()
                .ForMember(b => b.MatchName, opt => opt.MapFrom(m => m.Match.Name))
                .ForMember(b => b.Odds, opt => opt.MapFrom(b => b.Odds));
        }
    }
}