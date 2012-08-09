namespace InTheBoks.Worker
{
    using System.Reflection;
    using Autofac;
    using InTheBoks.Command;
    using InTheBoks.Data.Infrastructure;
    using InTheBoks.Data.Repositories;
    using InTheBoks.Dispatcher;

    public static class IoC
    {
        public static IContainer Current { get; set; }
    }

    public static class Bootstrapper
    {
        public static void Run()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<DefaultCommandBus>().As<ICommandBus>().SingleInstance();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().SingleInstance();
            builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>().SingleInstance();

            builder.RegisterAssemblyTypes(typeof(CatalogRepository).Assembly)
            .Where(t => t.Name.EndsWith("Repository"))
            .AsImplementedInterfaces().SingleInstance();

            var services = Assembly.Load("InTheBoks");
            builder.RegisterAssemblyTypes(services).AsClosedTypesOf(typeof(ICommandHandler<>)).SingleInstance();
            builder.RegisterAssemblyTypes(services).AsClosedTypesOf(typeof(IValidationHandler<>)).SingleInstance();

            IContainer container = builder.Build();
            IoC.Current = container;
        }
    }
}
