namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using ApplicationLifeCycle;

    using SimpleInjector;

    public class CompositeRoot : CompositeRootBase
    {
        protected override bool registerBindings()
        {
            this.GlobalContainer.Register<IBrowserContextFactory, BrowserContextFactory>(Lifestyle.Singleton);

            this.GlobalContainer.Register<IVendorToOperatorSendPointsTransferHandler, VendorToOperatorSendPointsTransferHandler>();

            return true;
        }
    }
}
