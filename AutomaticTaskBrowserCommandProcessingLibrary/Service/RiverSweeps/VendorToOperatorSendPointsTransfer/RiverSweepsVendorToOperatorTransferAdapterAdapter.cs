namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public class RiverSweepsVendorToOperatorTransferAdapterAdapter : IVendorToOperatorSendPointsTransferAdapter
    {
        private readonly IRiverSweepsVendorToOperatorSendPointsTransfer riverSweepsVendorToOperatorSendPointsTransfer;

        public RiverSweepsVendorToOperatorTransferAdapterAdapter(IRiverSweepsVendorToOperatorSendPointsTransfer riverSweepsVendorToOperatorSendPointsTransfer)
        {
            this.riverSweepsVendorToOperatorSendPointsTransfer = riverSweepsVendorToOperatorSendPointsTransfer;
        }

        VendorToOperatorSendPointsTransferResponse IVendorToOperatorSendPointsTransferAdapter.Execute(IWebDriver driver, VendorToOperatorSendPointsTransferRequest vendorToOperatorSendPointsTransferRequest)
        {
            var response = this.riverSweepsVendorToOperatorSendPointsTransfer.Execute(driver, vendorToOperatorSendPointsTransferRequest);
            return new VendorToOperatorSendPointsTransferResponse() { IsSuccessful = response.IsSuccessful };
        }
    }
}
