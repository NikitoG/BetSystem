namespace BetSystem.Web.Api.Controllers
{
    using Web.Infrastructure.Mapping;
    using Models.Sports;
    using Services.Data;
    using System.Linq;
    using System.Web.Http;
    using Models.Games;
    using System.Web.Http.Cors;
    
    public class SportsController : BaseController
    {
        private readonly ISportsService sports;

        private readonly IGamesSevice games;

        public SportsController(ISportsService sports, IGamesSevice games)
        {
            this.sports = sports;
            this.games = games;
        }

        public IHttpActionResult Get()
        {
            var allSports = this.sports.GetAllSports()
                .To<ListedSportsResponseModel>()
                .ToList();

            return this.Ok(allSports);
        }

        public IHttpActionResult Get(int id, bool all = false)
        {
            var allGames = this.games.GetAllMatchesBySportId(id, all)
                .To<AllGamesBySportResponceView>()
                .ToList();

            return this.Ok(allGames);
        }
    }
}