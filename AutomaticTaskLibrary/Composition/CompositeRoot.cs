namespace AutomaticTaskLibrary
{
    using ApplicationLifeCycle;

    using InvoiceRepository;

    using InvoiceRepositoryTypes;

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
