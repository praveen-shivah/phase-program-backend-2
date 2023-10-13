namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;
    
    public class ResellerPlayersRetrieveChainStart : IResellerPlayersRetrieveChain
    {
        ResellerPlayersRetrieveResponse IResellerPlayersRetrieveChain.Execute(IWebDriver driver, ResellerPlayersRetrieveRequest resellerPlayersRetrieveRequest)
        {
            return new ResellerPlayersRetrieveResponse
            {
                IsSuccessful = true,
                ResponseType = ResellerPlayersRetrieveResponseType.start
            };
        }
    }
}
