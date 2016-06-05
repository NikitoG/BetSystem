namespace BetSystem.Web.Api.Models.Odds
{
    using System;
    using AutoMapper;
    using BetSystem.Web.Infrastructure.Mapping;
    using BetSystem.Data.Models;

    public class OddResponseModel : IMapFrom<Odd>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public int Key { get; set; }
        
        public string Name { get; set; }

        public decimal? SpecialBetValue { get; set; }

        public decimal Value { get; set; }

        public string BetName { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Odd, OddResponseModel>()
                .ForMember(o => o.BetName, opt => opt.MapFrom(b => b.Bet.Name));
        }
    }
}