namespace AutomaticTaskMessageHandlerHost
{
    using ApplicationLifeCycle;

    using AutomaticTaskSharedLibrary;

    using DatabaseClassLibrary;

    using DataPostgresqlLibrary;

    using LoggingLibrary;

    using Microsoft.Extensions.Configuration;

    using SharedUtilities;

    using SimpleInjector;

    using UnitOfWorkTypesLibrary;

    using LoggerAdapterFactory = LoggingServicesLibrary.LoggerAdapterFactory;

    public class CompositeRoot : CompositeRootBase
    {
        protected override bool registerBindings()
        {
            this.GlobalContainer.Register<ILoggerAdapterFactory, LoggerAdapterFactory>(Lifestyle.Singleton);
            this.GlobalContainer.Register<ILoggerFactory, LoggerFactory>(Lifestyle.Singleton);
            this.GlobalContainer.Register<IConnectionFactory, ConnectionFactoryNormal>(Lifestyle.Singleton);
            this.GlobalContainer.Register<IEntityContextFrameWorkFactory<DPContext>, EntityContextFrameWorkFactoryNormal>(Lifestyle.Singleton);
            this.GlobalContainer.RegisterInstance<IConfiguration>(new ConfigurationBuilder().Build());
            this.GlobalContainer.Register<IGuidFactory, GuidFactory>(Lifestyle.Singleton);

            this.GlobalContainer.Register<IEndpointConfigurationFactory, EndpointConfigurationFactoryTestingLocal>();

            return true;
        }
    }
}
