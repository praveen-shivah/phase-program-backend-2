namespace AutomaticTaskMessageLibrary
{
    using ApplicationLifeCycle;

    using AutomaticTaskLibrary;

    using AutomaticTaskSharedLibrary;

    using InvoiceRepository;

    using SimpleInjector;

    public class CompositeRoot : CompositeRootBase
    {
        protected override bool registerBindings()
        {
            this.GlobalContainer.Register<IDistributorToOperatorSendPointsTransfer, DistributorToResellerSendPointsTransfer>(Lifestyle.Transient);
            this.GlobalContainer.Register<IResellerBalanceRetrieve, ResellerBalanceRetrieve>(Lifestyle.Transient);

            this.GlobalContainer.Register<IPlaceMessageOnServiceBus, PlaceMessageOnServiceBus>(Lifestyle.Transient);
            this.GlobalContainer.Register<IEndpointConfigurationFactory, EndpointConfigurationFactoryTestingLocal>(Lifestyle.Transient);
            return true;
        }
    }
}
