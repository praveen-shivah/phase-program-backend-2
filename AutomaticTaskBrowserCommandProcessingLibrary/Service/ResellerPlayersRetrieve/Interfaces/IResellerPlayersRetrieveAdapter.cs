namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;
    public interface IResellerPlayersRetrieveAdapter
    {
        ResellerPlayersRetrieveResponse Execute(IWebDriver driver, ResellerPlayersRetrieveRequest resellerPlayersRetrieveRequest);
    }
}
