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
            this.GlobalContainer.Register<IVendorToOperatorSendPointsTransferAdapter, VendorToOperatorTransferAdapter>();

            this.GlobalContainer.Register<IVendorBalanceRetrieveHandler, VendorBalanceRetrieveHandler>();
            this.GlobalContainer.Register<IVendorBalanceRetrieveAdapter, VendorBalanceRetrieveAdapter>();

            this.GlobalContainer.Register<ILoginPageFactory, LoginPageFactory>();

            this.GlobalContainer.Register<IVendorToOperatorSendPointsTransferChain, VendorToOperatorSendPointsTransferStart>();
            this.GlobalContainer.RegisterDecorator<IVendorToOperatorSendPointsTransferChain, VendorToOperatorSendPointsTransferLoginCreate>();
            this.GlobalContainer.RegisterDecorator<IVendorToOperatorSendPointsTransferChain, VendorToOperatorSendPointsTransferLoginVerifyLoad>();
            this.GlobalContainer.RegisterDecorator<IVendorToOperatorSendPointsTransferChain, VendorToOperatorSendPointsTransferLoginSubmit>();
            this.GlobalContainer.RegisterDecorator<IVendorToOperatorSendPointsTransferChain, VendorToOperatorSendPointsTransferManagementVerifyFundsAvailable>();
            this.GlobalContainer.RegisterDecorator<IVendorToOperatorSendPointsTransferChain, VendorToOperatorSendPointsTransferManagementLocateDepositBtn>();
            this.GlobalContainer.RegisterDecorator<IVendorToOperatorSendPointsTransferChain, VendorToOperatorSendPointsTransferManagementMakeDeposit>();

            this.GlobalContainer.Register<IVendorBalanceRetrieveChain, VendorBalanceRetrieveChainStart>();
            this.GlobalContainer.RegisterDecorator<IVendorBalanceRetrieveChain, VendorBalanceRetrieveChainLoginCreate>();
            this.GlobalContainer.RegisterDecorator<IVendorBalanceRetrieveChain, VendorBalanceRetrieveChainLoginVerifyLoad>();
            this.GlobalContainer.RegisterDecorator<IVendorBalanceRetrieveChain, VendorBalanceRetrieveChainLoginSubmit>();
            this.GlobalContainer.RegisterDecorator<IVendorBalanceRetrieveChain, VendorBalanceRetrieveManagementGetBalance>();

            return true;
        }
    }
}
