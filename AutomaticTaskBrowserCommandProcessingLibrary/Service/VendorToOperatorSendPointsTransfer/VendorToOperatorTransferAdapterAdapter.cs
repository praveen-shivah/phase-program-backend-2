namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public class VendorToOperatorTransferAdapterAdapter : IVendorToOperatorSendPointsTransferAdapter
    {
        private readonly IVendorToOperatorSendPointsTransferChain vendorToOperatorSendPointsTransferChain;

        public VendorToOperatorTransferAdapterAdapter(IVendorToOperatorSendPointsTransferChain vendorToOperatorSendPointsTransferChain)
        {
            this.vendorToOperatorSendPointsTransferChain = vendorToOperatorSendPointsTransferChain;
        }

        VendorToOperatorSendPointsTransferResponse IVendorToOperatorSendPointsTransferAdapter.Execute(IWebDriver driver, VendorToOperatorSendPointsTransferRequest vendorToOperatorSendPointsTransferRequest)
        {
            var response = this.vendorToOperatorSendPointsTransferChain.Execute(driver, vendorToOperatorSendPointsTransferRequest);
            return new VendorToOperatorSendPointsTransferResponse() { IsSuccessful = response.IsSuccessful };
        }
    }
}
