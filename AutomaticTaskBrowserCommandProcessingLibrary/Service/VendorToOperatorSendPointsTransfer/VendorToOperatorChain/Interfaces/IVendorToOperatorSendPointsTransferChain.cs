namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public interface IVendorToOperatorSendPointsTransferChain
    {
        VendorToOperatorTransferResponse Execute(IWebDriver driver, VendorToOperatorSendPointsTransferRequest vendorToOperatorSendPointsTransferRequest);
    }
}
