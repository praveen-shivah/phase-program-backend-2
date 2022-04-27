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

            return true;
        }
    }
}
