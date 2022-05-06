namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public interface IResellerBalanceRetrieveAdapter
    {
        ResellerBalanceRetrieveResponse Execute(IWebDriver driver, ResellerBalanceRetrieveRequest vendorBalanceRetrieveRequest);
    }
}
