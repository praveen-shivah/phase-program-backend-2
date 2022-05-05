namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public interface IVendorToOperatorSendPointsTransferAdapter
    {
        VendorToOperatorTransferResponse Execute(IWebDriver driver, VendorToOperatorSendPointsTransferRequest vendorToOperatorSendPointsTransferRequest);
    }
}
