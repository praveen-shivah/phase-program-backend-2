namespace AutomaticTaskMessageLibrary
{
    using ApplicationLifeCycle;

    using AutomaticTaskLibrary;

    using InvoiceRepository;

    using SimpleInjector;

    public class CompositeRoot : CompositeRootBase
    {
        protected override bool registerBindings()
        {
            this.GlobalContainer.Register<IDistributorToOperatorSendPointsTransfer, DistributorToResellerSendPointsTransfer>(Lifestyle.Singleton);
            this.GlobalContainer.Register<IResellerBalanceRetrieve, ResellerBalanceRetrieve>(Lifestyle.Singleton);

            this.GlobalContainer.Register<IPlaceMessageOnServiceBus, PlaceMessageOnServiceBus>(Lifestyle.Singleton);
            this.GlobalContainer.Register<IEndpointConfigurationFactory, EndpointConfigurationFactoryTestingLocal>(Lifestyle.Singleton);
            return true;
        }
    }
}
