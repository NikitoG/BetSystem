namespace BetSystem.Web.Api.App_Start
{
    using System.Reflection;

    using Autofac;

    using Controllers;

    using Data;
    using Data.Common;

    using Services.Data;
    using Services.Web;
    using Autofac.Integration.WebApi;
    using System.Web.Http;
    using System.Data.Entity;
    using Infrastructure.RssFeed;
    public static class AutofacConfig
    {
        public static void RegisterAutofac()
        {
            var builder = new ContainerBuilder();

            // Get your HttpConfiguration.
            var config = GlobalConfiguration.Configuration;

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // OPTIONAL: Register the Autofac filter provider.
            builder.RegisterWebApiFilterProvider(config);
            
            // Register services
            RegisterServices(builder);

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.Register(x => new BetSystemDbContext())
                .As<IBetSystemDbContext>()
                .InstancePerRequest();

            builder.Register(x => new BetSystemDbContext())
                .As<DbContext>()
                .InstancePerRequest();

            builder.Register(x => new HttpCacheService())
                .As<ICacheService>()
                .InstancePerRequest();

            builder.RegisterGeneric(typeof(DbRepository<>))
                .As(typeof(IDbRepository<>))
                .InstancePerRequest();
            
            var servicesAssembly = Assembly.GetAssembly(typeof(ISportsService));
            builder.RegisterAssemblyTypes(servicesAssembly).AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .AssignableTo<BaseController>().PropertiesAutowired();
        }
    }
}