namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using ApplicationLifeCycle;
    using AutomaticTaskBrowserCommandProcessingLibrary.Service.ResellerTransactionRetrieve.ResellerTransactionRetrieveChain;
    using SimpleInjector;

    public class CompositeRoot : CompositeRootBase
    {
        protected override bool registerBindings()
        {
            this.GlobalContainer.Register<IBrowserContextFactory, BrowserContextFactory>(Lifestyle.Singleton);

            this.GlobalContainer.Register<IDistributorToResellerSendPointsTransferProcessor, DistributorToResellerSendPointsTransferProcessor>();
            this.GlobalContainer.Register<IDistributorToResellerSendPointsTransferAdapter, DistributorToResellerTransferAdapter>();

            this.GlobalContainer.Register<IResellerBalanceRetrieveProcessor, ResellerBalanceRetrieveProcessor>();
            this.GlobalContainer.Register<IResellerBalanceRetrieveAdapter, ResellerBalanceRetrieveAdapter>();

            this.GlobalContainer.Register<IResellerTransactionRetrieveProcessor, ResellerTransactionRetrieveProcessor>();
            this.GlobalContainer.Register<IResellerTransactionRetrieveAdapter, ResellerTransactionRetrieveAdapter>();

            this.GlobalContainer.Register<IResellerPlayersRetrieveProcessor, ResellerPlayersRetrieveProcessor>();
            this.GlobalContainer.Register<IResellerPlayersRetrieveAdapter, ResellerPlayersRetrieveAdapter>();

            this.GlobalContainer.Register<ILoginPageFactory, LoginPageFactory>();
            this.GlobalContainer.Register<ILogoutPageFactory, LogoutPageFactory>();
            this.GlobalContainer.Register<IManagementPageFactory, ManagementPageFactory>();
            this.GlobalContainer.Register<IResellerBalancePageFactory, ResellerBalancePageFactory>();
            this.GlobalContainer.Register<ITransactionReportsPageFactory, TransactionReportsPageFactory>();
            this.GlobalContainer.Register<IResellerPlayerPageFactory, PlayersPageFactory>();

            this.GlobalContainer.Register<IDistributorToResellerSendPointsTransferChain, DistributorToResellerSendPointsTransferStart>();
            this.GlobalContainer.RegisterDecorator<IDistributorToResellerSendPointsTransferChain, DistributorToResellerSendPointsTransferLoginCreate>();
            this.GlobalContainer.RegisterDecorator<IDistributorToResellerSendPointsTransferChain, DistributorToResellerSendPointsTransferLoginVerifyLoad>();
            this.GlobalContainer.RegisterDecorator<IDistributorToResellerSendPointsTransferChain, DistributorToResellerSendPointsTransferLoginSubmit>();
            this.GlobalContainer.RegisterDecorator<IDistributorToResellerSendPointsTransferChain, DistributorToResellerSendPointsTransferManagementPageCreate>();
            this.GlobalContainer.RegisterDecorator<IDistributorToResellerSendPointsTransferChain, DistributorToResellerSendPointsTransferManagementPageVerifyLoad>();
            this.GlobalContainer.RegisterDecorator<IDistributorToResellerSendPointsTransferChain, DistributorToResellerSendPointsTransferManagementVerifyFundsAvailable>();
            this.GlobalContainer.RegisterDecorator<IDistributorToResellerSendPointsTransferChain, DistributorToResellerSendPointsTransferManagementLocateDepositBtn>();
            this.GlobalContainer.RegisterDecorator<IDistributorToResellerSendPointsTransferChain, DistributorToResellerSendPointsTransferManagementMakeDeposit>();
            this.GlobalContainer.RegisterDecorator<IDistributorToResellerSendPointsTransferChain, DistributorToResellerSendPointsTransferUpdateApi>();
            
            //this.GlobalContainer.RegisterDecorator<IDistributorToResellerSendPointsTransferChain, DistributorToResellerSendPointsTransferLogoutCreate>();
            //this.GlobalContainer.RegisterDecorator<IDistributorToResellerSendPointsTransferChain, DistributorToResellerSendPointsTransferLogoutVerifyLoad>();

            this.GlobalContainer.Register<IResellerBalanceRetrieveChain, ResellerBalanceRetrieveChainStart>();
            this.GlobalContainer.RegisterDecorator<IResellerBalanceRetrieveChain, ResellerBalanceRetrieveChainLoginCreate>();
            this.GlobalContainer.RegisterDecorator<IResellerBalanceRetrieveChain, ResellerBalanceRetrieveChainLoginVerifyLoad>();
            this.GlobalContainer.RegisterDecorator<IResellerBalanceRetrieveChain, ResellerBalanceRetrieveChainLoginSubmit>();
            this.GlobalContainer.RegisterDecorator<IResellerBalanceRetrieveChain, ResellerBalanceRetrieveManagementGetBalance>();
            this.GlobalContainer.RegisterDecorator<IResellerBalanceRetrieveChain, ResellerBalanceRetrieveUpdateApi>();
            this.GlobalContainer.RegisterDecorator<IResellerBalanceRetrieveChain, ResellerBalanceRetrieveChainLogoutCreate>();
            this.GlobalContainer.RegisterDecorator<IResellerBalanceRetrieveChain, ResellerBalanceRetrieveChainLogoutVerifyLoad>();

            this.GlobalContainer.Register<IResellerTransactionRetrieveChain, ResellerTransactionRetrieveChainStart>();
            this.GlobalContainer.RegisterDecorator<IResellerTransactionRetrieveChain, ResellerTransactionRetrieveChainLoginCreate>();
            this.GlobalContainer.RegisterDecorator<IResellerTransactionRetrieveChain, ResellerTransactionRetrieveChainLoginVerifyLoad>();
            this.GlobalContainer.RegisterDecorator<IResellerTransactionRetrieveChain, ResellerTransactionRetrieveChainLoginSubmit>();
            this.GlobalContainer.RegisterDecorator<IResellerTransactionRetrieveChain, ResellerTransactionRetrieveChainTransactionReportsCreate>();
            this.GlobalContainer.RegisterDecorator<IResellerTransactionRetrieveChain, ResellerTransactionRetrieveChainTransactionReportsVerifyLoad>();
            this.GlobalContainer.RegisterDecorator<IResellerTransactionRetrieveChain, ResellerTransactionRetrieveChainTransactionReportsRetrive>();
            this.GlobalContainer.RegisterDecorator<IResellerTransactionRetrieveChain, ResellerTransactionRetrieveChainLogoutCreate>();
            this.GlobalContainer.RegisterDecorator<IResellerTransactionRetrieveChain, ResellerTransactionRetrieveChainLogoutVerifyLoad>();

            this.GlobalContainer.Register<IResellerPlayersRetrieveChain, ResellerPlayersRetrieveChainStart>();
            this.GlobalContainer.RegisterDecorator<IResellerPlayersRetrieveChain, ResellerPlayersRetrieveChainLoginCreate>();
            this.GlobalContainer.RegisterDecorator<IResellerPlayersRetrieveChain, ResellerPlayersRetrieveChainLoginVerifyLoad>();
            this.GlobalContainer.RegisterDecorator<IResellerPlayersRetrieveChain, ResellerPlayersRetrieveChainLoginSubmit>();
            this.GlobalContainer.RegisterDecorator<IResellerPlayersRetrieveChain, ResellerPlayersRetrieveChainSavePlayer>();
            this.GlobalContainer.RegisterDecorator<IResellerPlayersRetrieveChain, ResellerPlayersRetrieveChainSavePlayerVerifyLoad>();
            this.GlobalContainer.RegisterDecorator<IResellerPlayersRetrieveChain, ResellerPlayersRetrieveChainLogoutCreate>();
            this.GlobalContainer.RegisterDecorator<IResellerPlayersRetrieveChain, ResellerPlayersRetrieveChainLogoutVerifyLoad>();

            return true;
        }
    }
}
