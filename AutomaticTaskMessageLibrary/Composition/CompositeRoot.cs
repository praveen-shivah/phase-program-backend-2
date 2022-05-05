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
            this.GlobalContainer.Register<IVendorToOperatorSendPointsTransfer, VendorToOperatorSendPointsTransfer>(Lifestyle.Singleton);
            this.GlobalContainer.Register<IVendorBalanceRetrieve, VendorBalanceRetrieve>(Lifestyle.Singleton);

            this.GlobalContainer.Register<IPlaceMessageOnServiceBus, PlaceMessageOnServiceBus>(Lifestyle.Singleton);
            this.GlobalContainer.Register<IEndpointConfigurationFactory, EndpointConfigurationFactoryTestingLocal>(Lifestyle.Singleton);
            return true;
        }
    }
}
