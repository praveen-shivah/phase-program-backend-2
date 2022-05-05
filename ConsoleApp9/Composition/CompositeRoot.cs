namespace ConsoleApp9
{
    using ApplicationLifeCycle;

    using AutomaticTaskLibrary;

    using DatabaseClassLibrary;

    using DataPostgresqlLibrary;

    using InvoiceRepository;

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
            this.GlobalContainer.Register<IVendorToOperatorSendPointsTransferTest, VendorToOperatorSendPointsTransferTest>(Lifestyle.Singleton);
            this.GlobalContainer.Register<IVendorBalanceRetrieveTest, VendorBalanceRetrieveTest>(Lifestyle.Singleton);

            this.GlobalContainer.Register<ILoggerAdapterFactory, LoggerAdapterFactory>(Lifestyle.Singleton);
            this.GlobalContainer.Register<ILoggerFactory, LoggerFactory>(Lifestyle.Singleton);
            this.GlobalContainer.Register<IConnectionFactory, ConnectionFactoryNormal>(Lifestyle.Singleton);
            this.GlobalContainer.Register<IEntityContextFrameWorkFactory<DPContext>, EntityContextFrameWorkFactoryNormal>(Lifestyle.Singleton);
            this.GlobalContainer.RegisterInstance<IConfiguration>(new ConfigurationBuilder().Build());
            this.GlobalContainer.Register<IGuidFactory, GuidFactory>(Lifestyle.Singleton);

            return true;
        }
    }
}
