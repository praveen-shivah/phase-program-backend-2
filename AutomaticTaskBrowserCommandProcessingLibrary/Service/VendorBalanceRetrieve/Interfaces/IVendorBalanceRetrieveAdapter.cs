namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public interface IVendorBalanceRetrieveAdapter
    {
        VendorBalanceRetrieveResponse Execute(IWebDriver driver, VendorBalanceRetrieveRequest vendorBalanceRetrieveRequest);
    }
}
