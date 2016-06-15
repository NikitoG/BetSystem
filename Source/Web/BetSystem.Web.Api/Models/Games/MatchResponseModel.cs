namespace BetSystem.Web.Api.Models.Games
{
    using System;
    using AutoMapper;
    using BetSystem.Web.Infrastructure.Mapping;
    using Data.Models;
    using System.Collections.Generic;
    using Bets;
    using System.Linq;

    public class MatchResponseModel : IMapFrom<Match>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public int Key { get; set; }

        public string Name { get; set; }

        public MatchType MatchType { get; set; }

        public DateTime StartDate { get; set; }

        public string Event { get; set; }

        public string Sport { get; set; }

        public string FirstPlayer
        {
            get
            {
                return this.Name.Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
            }
        }

        public string SecondPlayer
        {
            get
            {
                return this.Name.Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
            }
        }

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

        public virtual ICollection<BetResponceModel> Bets { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Match, MatchResponseModel>()
                .ForMember(g => g.Event, opt => opt.MapFrom(e => e.Event.Name))
                .ForMember(b => b.Bets, opt => opt.MapFrom(b => b.Bets))
                .ForMember(g => g.Sport, opt => opt.MapFrom(g => g.Event.Sport.Name));
        }
    }
}