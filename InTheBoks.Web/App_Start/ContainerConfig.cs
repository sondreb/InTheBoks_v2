namespace InTheBoks.Web
{
    using System.Reflection;
    using System.Web.Http;
    using System.Web.Mvc;
    using Autofac;
    using Autofac.Integration.Mvc;
    using Autofac.Integration.WebApi;
    using InTheBoks.Command;
    using InTheBoks.Data.Infrastructure;
    using InTheBoks.Data.Repositories;
    using InTheBoks.Dispatcher;

    public class ContainerConfig
    {
        public static void RegisterContainer(HttpConfiguration configuration)
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            //builder.RegisterType<DefaultCommandBus>().As<ICommandBus>().InstancePerApiRequest();
            builder.RegisterType<DefaultCommandBus>().As<ICommandBus>().InstancePerHttpRequest();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerHttpRequest();
            builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>().InstancePerHttpRequest();

            builder.RegisterAssemblyTypes(typeof(CatalogRepository).Assembly)
            .Where(t => t.Name.EndsWith("Repository"))
            .AsImplementedInterfaces().InstancePerHttpRequest();

            var services = Assembly.Load("InTheBoks");
            builder.RegisterAssemblyTypes(services).AsClosedTypesOf(typeof(ICommandHandler<>)).InstancePerHttpRequest();
            builder.RegisterAssemblyTypes(services).AsClosedTypesOf(typeof(IValidationHandler<>)).InstancePerHttpRequest();

            //builder.RegisterType<DefaultFormsAuthentication>().As<IFormsAuthentication>().InstancePerHttpRequest();
            builder.RegisterFilterProvider();
            IContainer container = builder.Build();

            // Set the dependency resolver implementation.
            var resolver = new AutofacWebApiDependencyResolver(container);
            configuration.DependencyResolver = resolver;
        }
    }
}