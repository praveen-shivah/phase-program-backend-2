namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public class VendorToOperatorTransferAdapter : IVendorToOperatorSendPointsTransferAdapter
    {
        private readonly IVendorToOperatorSendPointsTransferChain vendorToOperatorSendPointsTransferChain;

        public VendorToOperatorTransferAdapter(IVendorToOperatorSendPointsTransferChain vendorToOperatorSendPointsTransferChain)
        {
            this.vendorToOperatorSendPointsTransferChain = vendorToOperatorSendPointsTransferChain;
        }

        VendorToOperatorTransferResponse IVendorToOperatorSendPointsTransferAdapter.Execute(IWebDriver driver, VendorToOperatorSendPointsTransferRequest vendorToOperatorSendPointsTransferRequest)
        {
            return this.vendorToOperatorSendPointsTransferChain.Execute(driver, vendorToOperatorSendPointsTransferRequest);
        }
    }
}
