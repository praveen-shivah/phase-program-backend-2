﻿namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using ApplicationLifeCycle;

    using SimpleInjector;

    public class CompositeRoot : CompositeRootBase
    {
        protected override bool registerBindings()
        {
            this.GlobalContainer.Register<IBrowserContextFactory, BrowserContextFactory>(Lifestyle.Singleton);

            this.GlobalContainer.Register<IVendorToOperatorSendPointsTransferHandler, VendorToOperatorSendPointsTransferHandler>();
            this.GlobalContainer.Register<IVendorToOperatorSendPointsTransferAdapter, VendorToOperatorTransferAdapterAdapter>();
            this.GlobalContainer.Register<IVendorToOperatorTransferLoginPageFactory, VendorToOperatorTransferLoginPageFactory>();

            this.GlobalContainer.Register<IVendorToOperatorSendPointsTransferChain, VendorToOperatorSendPointsTransferStart>();
            this.GlobalContainer.RegisterDecorator<IVendorToOperatorSendPointsTransferChain, VendorToOperatorSendPointsTransferLoginCreate>();
            this.GlobalContainer.RegisterDecorator<IVendorToOperatorSendPointsTransferChain, VendorToOperatorSendPointsTransferLoginVerifyLoad>();
            this.GlobalContainer.RegisterDecorator<IVendorToOperatorSendPointsTransferChain, VendorToOperatorSendPointsTransferLoginSubmit>();
            this.GlobalContainer.RegisterDecorator<IVendorToOperatorSendPointsTransferChain, VendorToOperatorSendPointsTransferManagementVerifyFundsAvailable>();
            this.GlobalContainer.RegisterDecorator<IVendorToOperatorSendPointsTransferChain, VendorToOperatorSendPointsTransferManagementLocateDepositBtn>();
            this.GlobalContainer.RegisterDecorator<IVendorToOperatorSendPointsTransferChain, VendorToOperatorSendPointsTransferManagementMakeDeposit>();

            return true;
        }
    }
}
