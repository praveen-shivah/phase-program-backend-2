namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;
    public class ResellerPlayersRetrieveAdapter : IResellerPlayersRetrieveAdapter
    {
        private readonly IResellerPlayersRetrieveChain resellerPlayersRetrieveChain;
        public ResellerPlayersRetrieveAdapter(IResellerPlayersRetrieveChain resellerPlayersRetrieveChain)
        {
            this.resellerPlayersRetrieveChain = resellerPlayersRetrieveChain;
        }
        public ResellerPlayersRetrieveResponse Execute(IWebDriver driver, ResellerPlayersRetrieveRequest resellerPlayersRetrieveRequest)
        {
            return this.resellerPlayersRetrieveChain.Execute(driver, resellerPlayersRetrieveRequest);
        }
    }
}
