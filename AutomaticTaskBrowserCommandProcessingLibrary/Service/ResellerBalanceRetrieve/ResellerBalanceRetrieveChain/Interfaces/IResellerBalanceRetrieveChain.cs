namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public interface IResellerBalanceRetrieveChain
    {
        ResellerBalanceRetrieveResponse Execute(IWebDriver driver, ResellerBalanceRetrieveRequest resellerBalanceRetrieveRequest);
    }
}
