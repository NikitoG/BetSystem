namespace BetSystem.Web.Api.Controllers
{
    using Infrastructure.Mapping;
    using Models.Statistics;
    using Services.Data;
    using System.Linq;
    using System.Web.Http;

    public class StatsController : BaseController
    {
        private readonly ISportsService sports;

        public StatsController(ISportsService sports)
        {
            this.sports = sports;
        }

        [HttpGet]
        public StatsViewModel Get()
        {
            var allSports = this.sports.GetAll()
                .To<StatsViewModel>()
                .ToList();

            var result = new StatsViewModel()
            {
                Sports = 0,
                Events = 0,
                Games = 0,
                Bets = 0,
                Odds = 0
            };

            foreach (var sport in allSports)
            {
                ++result.Sports;
                result.Events += sport.Events;
                result.Games += sport.Games;
                result.Bets += sport.Bets;
                result.Odds += sport.Odds;
            }

            return result;
        }
    }
}
