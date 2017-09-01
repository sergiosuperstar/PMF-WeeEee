using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using DailyPlanning.Infrastructure.Context;
using DailyPlanning.Infrastructure.Database;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DailyPlanning
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            System.Data.Entity.Database.SetInitializer(new DailyPlanningDBInitializer());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            // Dependency injection:
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // Register types:
            builder.RegisterType<DailyPlanningContext>()
                   .AsSelf()
                   .InstancePerLifetimeScope();

            var container = builder.Build();

            // Register resolver for asp.net mvc:
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
