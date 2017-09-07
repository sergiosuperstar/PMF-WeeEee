using Autofac;
using Autofac.Integration.Mvc;
using DailyPlanning.Infrastructure.Context;
using DailyPlanning.Infrastructure.Database;
using DailyPlanning.MapperConfig;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DailyPlanning
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            DailyPlanningInitializationHandler.Initialize();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var mapper = MapperProvider.Initialize().CreateMapper();

            // Dependency injection:
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // Register types:
            builder.RegisterType<DailyPlanningContext>()
                   .AsSelf()
                   .InstancePerLifetimeScope();

            builder.RegisterInstance(mapper);

            var container = builder.Build();

            // Register resolver for asp.net mvc:
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
