namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using ApplicationLifeCycle;

    using SimpleInjector;

    public class CompositeRoot : CompositeRootBase
    {
        protected override bool registerBindings()
        {
            this.GlobalContainer.Register<IBrowserContextFactory, BrowserContextFactory>(Lifestyle.Singleton);

            this.GlobalContainer.Register<IDistributorToResellerSendPointsTransferHandler, DistributorToResellerSendPointsTransferHandler>();
            this.GlobalContainer.Register<IDistributorToResellerSendPointsTransferAdapter, DistributorToResellerTransferAdapter>();

            this.GlobalContainer.Register<IResellerBalanceRetrieveHandler, ResellerBalanceRetrieveHandler>();
            this.GlobalContainer.Register<IResellerBalanceRetrieveAdapter, ResellerBalanceRetrieveAdapter>();

            this.GlobalContainer.Register<ILoginPageFactory, LoginPageFactory>();
            this.GlobalContainer.Register<IManagementPageFactory, ManagementPageFactory>();
            this.GlobalContainer.Register<IResellerBalancePageFactory, ResellerBalancePageFactory>();

            this.GlobalContainer.Register<IDistributorToResellerSendPointsTransferChain, DistributorToResellerSendPointsTransferStart>();
            this.GlobalContainer.RegisterDecorator<IDistributorToResellerSendPointsTransferChain, DistributorToResellerSendPointsTransferLoginCreate>();
            this.GlobalContainer.RegisterDecorator<IDistributorToResellerSendPointsTransferChain, DistributorToResellerSendPointsTransferLoginVerifyLoad>();
            this.GlobalContainer.RegisterDecorator<IDistributorToResellerSendPointsTransferChain, DistributorToResellerSendPointsTransferLoginSubmit>();
            this.GlobalContainer.RegisterDecorator<IDistributorToResellerSendPointsTransferChain, DistributorToResellerSendPointsTransferManagementPageCreate>();
            this.GlobalContainer.RegisterDecorator<IDistributorToResellerSendPointsTransferChain, DistributorToResellerSendPointsTransferManagementPageVerifyLoad>();
            this.GlobalContainer.RegisterDecorator<IDistributorToResellerSendPointsTransferChain, DistributorToResellerSendPointsTransferManagementVerifyFundsAvailable>();
            this.GlobalContainer.RegisterDecorator<IDistributorToResellerSendPointsTransferChain, DistributorToResellerSendPointsTransferManagementLocateDepositBtn>();
            this.GlobalContainer.RegisterDecorator<IDistributorToResellerSendPointsTransferChain, DistributorToResellerSendPointsTransferManagementMakeDeposit>();

            this.GlobalContainer.Register<IResellerBalanceRetrieveChain, ResellerBalanceRetrieveChainStart>();
            this.GlobalContainer.RegisterDecorator<IResellerBalanceRetrieveChain, ResellerBalanceRetrieveChainLoginCreate>();
            this.GlobalContainer.RegisterDecorator<IResellerBalanceRetrieveChain, ResellerBalanceRetrieveChainLoginVerifyLoad>();
            this.GlobalContainer.RegisterDecorator<IResellerBalanceRetrieveChain, ResellerBalanceRetrieveChainLoginSubmit>();
            this.GlobalContainer.RegisterDecorator<IResellerBalanceRetrieveChain, ResellerBalanceRetrieveManagementGetBalance>();

            return true;
        }
    }
}
