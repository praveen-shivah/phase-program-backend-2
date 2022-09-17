namespace MobileRequestApi
{
    using ApiHost.Middleware;

    using ApplicationLifeCycle;

    using AutomaticTaskSharedLibrary;

    using DatabaseClassLibrary;

    using DataPostgresqlLibrary;

    using InvoiceRepositoryTypes;

    using LoggingLibrary;

    using Microsoft.Extensions.Configuration;

    using SecurityUtilities;

    using SecurityUtilitiesTypes;

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
            this.GlobalContainer.Register<ISecretKeyRetrieval, SecretKeyRetrievalSettingsFile>(Lifestyle.Singleton);

            this.GlobalContainer.Register<IGuidFactory, GuidFactory>(Lifestyle.Singleton);
            this.GlobalContainer.RegisterInstance<IConfiguration>(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build());
            this.GlobalContainer.Register<IConnectionFactory, ConnectionFactoryNormal>(Lifestyle.Singleton);
            this.GlobalContainer.Register<IEntityContextFrameWorkFactory<DPContext>, EntityContextFrameWorkFactoryNormal>(Lifestyle.Singleton);

            return true;
        }
    }
}
