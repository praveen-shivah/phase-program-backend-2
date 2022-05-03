namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public class RiverSweepsVendorToOperatorSendPointsTransferStart : IRiverSweepsVendorToOperatorSendPointsTransfer
    {
        RiverSweepsVendorToOperatorTransferResponse IRiverSweepsVendorToOperatorSendPointsTransfer.Execute(
            IWebDriver driver,
            VendorToOperatorSendPointsTransferRequest vendorToOperatorSendPointsTransferRequest)
        {
            return new RiverSweepsVendorToOperatorTransferResponse
            {
                IsSuccessful = true,
                RiverSweepsVendorToOperatorTransferResponseType = RiverSweepsVendorToOperatorTransferResponseType.start
            };
        }
    }
}