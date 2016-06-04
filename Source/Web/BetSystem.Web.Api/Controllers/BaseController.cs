namespace BetSystem.Web.Api.Controllers
{
    using BetSystem.Services.Web;
    using System.Web.Http;

    public abstract class BaseController : ApiController
    {
        public ICacheService Cache { get; set; }
    }
}
