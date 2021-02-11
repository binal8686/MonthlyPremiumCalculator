using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using PremiumCalculator.DAL.DataModel;
using System.Data.Entity;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;

namespace PremiumCalculator.WebAPI
{
    public class AutofacWebApiConfig
    {
        public static IContainer Container;

        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }

        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<InsuranceDbEntities>()
                   .As<DbContext>()
                   .InstancePerLifetimeScope();

            // BAL Services
            builder.RegisterAssemblyTypes(Assembly.Load("PremiumCalculator.BAL"))
                   .Where(t => t.Name.EndsWith("BAL"))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();

            // DAL Services
            builder.RegisterAssemblyTypes(Assembly.Load("PremiumCalculator.DAL"))
                .Where(t => t.Name.EndsWith("DAL"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            Container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(Container));

            return Container;
        }
    }
}

