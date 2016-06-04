namespace BetSystem.Web.Api.Controllers
{
    using Ninject;
    using Services.Data.Contracts;
    using System.Web.Http;

    public class SportsController : ApiController
    {
        [Inject]
        public ISportsService Sports { get; set; }

        public IHttpActionResult Get(int page = 1)
        {
            return this.Ok("Spors Controller!");
        }
    }
}