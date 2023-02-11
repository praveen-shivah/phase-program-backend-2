namespace ConsoleApp9
{
    using ApplicationLifeCycle;

    using DatabaseContext;

    using LoggingLibrary;

    using SharedUtilities;

    using SimpleInjector;

    using UnitOfWorkTypesLibrary;

    using LoggerAdapterFactory = LoggingServicesLibrary.LoggerAdapterFactory;

    public class CompositeRoot : CompositeRootBase
    {
        protected override bool registerBindings()
        {
            this.GlobalContainer.Register<IDistributorToResellerSendPointsTransferTest, DistributorToResellerSendPointsTransferTest>(Lifestyle.Scoped);
            this.GlobalContainer.Register<IResellerBalanceRetrieveTest, ResellerBalanceRetrieveTest>(Lifestyle.Scoped);

            this.GlobalContainer.Register<ILoggerAdapterFactory, LoggerAdapterFactory>(Lifestyle.Singleton);
            this.GlobalContainer.Register<ILoggerFactory, LoggerFactory>(Lifestyle.Singleton);
            this.GlobalContainer.Register<IConnectionFactory, ConnectionFactoryNormal>(Lifestyle.Singleton);
            this.GlobalContainer.Register<IEntityContextFrameWorkFactory<DataContext>, EntityContextFrameWorkFactoryNormal>(Lifestyle.Singleton);
            this.GlobalContainer.Register<IGuidFactory, GuidFactory>(Lifestyle.Singleton);

            return true;
        }
    }
}
