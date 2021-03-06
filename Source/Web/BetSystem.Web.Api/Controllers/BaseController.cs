﻿namespace BetSystem.Web.Api.Controllers
{
    using AutoMapper;
    using BetSystem.Services.Web;
    using Infrastructure.Mapping;
    using System.Web.Http;
    using System.Web.Http.Cors;
    
    public abstract class BaseController : ApiController
    {
        public ICacheService Cache { get; set; }

        protected IMapper Mapper
        {
            get
            {
                return AutoMapperConfig.Configuration.CreateMapper();
            }
        }
    }
}
