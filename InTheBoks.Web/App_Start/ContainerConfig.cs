namespace InTheBoks.Web
{
    using Autofac;
    using Autofac.Integration.Mvc;
    using Autofac.Integration.WebApi;
    using InTheBoks.Command;
    using InTheBoks.Data.Infrastructure;
    using InTheBoks.Data.Repositories;
    using InTheBoks.Dispatcher;
    using InTheBoks.Hubs;
    using Microsoft.AspNet.SignalR;
    using System.Reflection;
    using System.Web.Http;
    using System.Web.Mvc;

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
            //builder.RegisterAssemblyTypes(services).AsClosedTypesOf(typeof(ICommandSuccessHandler<>)).InstancePerHttpRequest();
            builder.RegisterAssemblyTypes(services).AsClosedTypesOf(typeof(IValidationHandler<>)).InstancePerHttpRequest();

            //builder.RegisterType<ActivitiesHub>().AsSelf().AsImplementedInterfaces().InstancePerLifetimeScope();
            //builder.RegisterType<CatalogsHub>().AsSelf().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<ActivitiesHub>().AsSelf();
            builder.RegisterType<CatalogsHub>().AsSelf();


            builder.RegisterFilterProvider();
            IContainer container = builder.Build();

            // Set the dependency resolver implementation.
            var resolver = new AutofacWebApiDependencyResolver(container);
            configuration.DependencyResolver = resolver;

            // Set the resolver for MVC Controllers.
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            // Set the resolver for SignalR.
            GlobalHost.DependencyResolver = new Autofac.Integration.SignalR.AutofacDependencyResolver(container);
        }
    }
}