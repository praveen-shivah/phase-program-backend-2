namespace AutomaticTaskMessageHandlerHost
{
    using ApplicationLifeCycle;
    using DatabaseContext;

    using LoggingLibrary;
    using Microsoft.Extensions.Configuration;
    using SharedUtilities;

    using SimpleInjector;
    using System;
    using UnitOfWorkTypesLibrary;

    public class CompositeRoot : CompositeRootBase
    {
        protected override bool registerBindings()
        {
            this.GlobalContainer.RegisterInstance(typeof(IConfiguration), this.buildConfig());

            this.GlobalContainer.Register<ILoggerAdapterFactory, LoggerAdapterFactory>(Lifestyle.Singleton);
            this.GlobalContainer.Register<ILoggerFactory, LoggerFactory>(Lifestyle.Singleton);
            this.GlobalContainer.Register<IGuidFactory, GuidFactory>(Lifestyle.Singleton);

            this.GlobalContainer.Register<IConnectionFactory, ConnectionFactoryNormal>(Lifestyle.Singleton);
            this.GlobalContainer.Register<IEntityContextFrameWorkFactory<DataContext>, EntityContextFrameWorkFactoryNormal>(Lifestyle.Singleton);

            return true;
        }
        private IConfiguration buildConfig()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile($"appsettings.json", true);
            IConfiguration config = builder.Build();
            return config;
        }

    }
}
