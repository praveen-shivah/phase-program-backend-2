namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;
    public interface IResellerPlayersRetrieveChain
    {
        ResellerPlayersRetrieveResponse Execute(IWebDriver driver, ResellerPlayersRetrieveRequest resellerPlayersRetrieveRequest);
    }
}
