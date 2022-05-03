namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public interface IRiverSweepsVendorToOperatorSendPointsTransfer
    {
        RiverSweepsVendorToOperatorTransferResponse Execute(IWebDriver driver, VendorToOperatorSendPointsTransferRequest vendorToOperatorSendPointsTransferRequest);
    }
}
