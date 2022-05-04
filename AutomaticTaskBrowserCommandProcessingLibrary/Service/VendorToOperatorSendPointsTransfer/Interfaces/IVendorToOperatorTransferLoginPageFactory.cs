namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using AutomaticTaskLibrary;

    using OpenQA.Selenium;

    public interface IVendorToOperatorTransferLoginPageFactory
    {
        IVendorToOperatorTransferLoginPage Create(
            IWebDriver webDriver,
            VendorToOperatorSendPointsTransferRequest vendorToOperatorSendPointsTransferRequest);
    }
}