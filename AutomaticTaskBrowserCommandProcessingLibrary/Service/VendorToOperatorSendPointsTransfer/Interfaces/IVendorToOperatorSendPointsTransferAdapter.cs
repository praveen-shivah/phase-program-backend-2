namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public interface IVendorToOperatorSendPointsTransferAdapter
    {
        VendorToOperatorSendPointsTransferResponse Execute(IWebDriver driver, VendorToOperatorSendPointsTransferRequest vendorToOperatorSendPointsTransferRequest);
    }
}
