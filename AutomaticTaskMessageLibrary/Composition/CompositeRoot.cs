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
            this.GlobalContainer.Register<IDistributorToOperatorSendPointsTransfer, DistributorToResellerSendPointsTransfer>(Lifestyle.Scoped);
            this.GlobalContainer.Register<IResellerBalanceRetrieve, ResellerBalanceRetrieve>(Lifestyle.Scoped);

            this.GlobalContainer.Register<IPlaceMessageOnServiceBus, PlaceMessageOnServiceBus>(Lifestyle.Scoped);
            this.GlobalContainer.Register<IEndpointConfigurationFactory, EndpointConfigurationFactoryTestingLocal>(Lifestyle.Scoped);
            return true;
        }
    }
}
