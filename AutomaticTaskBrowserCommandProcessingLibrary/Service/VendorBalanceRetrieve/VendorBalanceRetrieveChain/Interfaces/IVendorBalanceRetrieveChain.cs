namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public interface IVendorBalanceRetrieveChain
    {
        VendorBalanceRetrieveResponse Execute(IWebDriver driver, VendorBalanceRetrieveRequest vendorBalanceRetrieveRequest);
    }
}
