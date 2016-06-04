namespace BetSystem.Web.Api.Controllers
{
    using Services.Data;
    using System.Linq;
    using System.Web.Http;

    public class SportsController : BaseController
    {
        private readonly ISportsService sports;

        public SportsController(ISportsService sports)
        {
            this.sports = sports;
        }

        public IHttpActionResult Get(int page = 1)
        {
            var result = this.sports.GetAllSports().Count();
            return this.Ok(result);
        }
    }
}