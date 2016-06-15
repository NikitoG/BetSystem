namespace BetSystem.Web.Api.Controllers
{
    using Web.Infrastructure.Mapping;
    using Models.Sports;
    using Services.Data;
    using System.Linq;
    using System.Web.Http;
    using Models.Games;

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
            var allSports = this.sports.GetAll()
                .To<ListedSportsResponseModel>()
                .ToList();

            return this.Ok(allSports);
        }

        [Route("api/sports/{name}")]
        public IHttpActionResult GetBySport(string name, bool all = false)
        {
            var allGames = this.games.GetAllMatchesBySport(name, all)
                .To<AllGamesBySportResponceView>()
                .ToList();

            return this.Ok(allGames);
        }

        [Route("api/events/{eventKey}")]
        public IHttpActionResult GetByEvent(int eventKey, bool all = false)
        {
            var allGames = this.games.GetAllMatchesByEvent(eventKey, all)
                    .To<AllGamesBySportResponceView>()
                    .ToList();

            return this.Ok(allGames);
        }
    }
}