namespace BetSystem.Web.Api.Controllers
{
    using Infrastructure.Mapping;
    using Models.Games;
    using Services.Data;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    public class MatchController : BaseController
    {
        private readonly IGamesSevice games;

        public MatchController(IGamesSevice games)
        {
            this.games = games;
        }

        [Route("api/match/{key}")]
        public IHttpActionResult Get(int key)
        {
            var responseModel = this.games
                .GetDetails(key)
                .To<MatchResponseModel>()
                .FirstOrDefault();

            return this.Ok(responseModel);
        }
    }
}
