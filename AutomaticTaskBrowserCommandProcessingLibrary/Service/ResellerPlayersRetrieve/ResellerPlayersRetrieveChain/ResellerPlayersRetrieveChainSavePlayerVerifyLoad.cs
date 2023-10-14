namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;
    
    public class ResellerPlayersRetrieveChainSavePlayerVerifyLoad : IResellerPlayersRetrieveChain
    {
        private readonly IResellerPlayersRetrieveChain resellerPlayersRetrieveChain;
        public ResellerPlayersRetrieveChainSavePlayerVerifyLoad(IResellerPlayersRetrieveChain resellerPlayersRetrieveChain)
        {
            this.resellerPlayersRetrieveChain = resellerPlayersRetrieveChain;
        }
        ResellerPlayersRetrieveResponse IResellerPlayersRetrieveChain.Execute(IWebDriver driver, ResellerPlayersRetrieveRequest resellerPlayersRetrieveRequest)
        {
            var response = this.resellerPlayersRetrieveChain.Execute(driver, resellerPlayersRetrieveRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            response.ResponseType = ResellerPlayersRetrieveResponseType.playersReportVerifyLoad;

            if (response.PlayersReportsPage.IsPageUrlSet() && response.PlayersReportsPage.VerifyPageLoaded())
            {
                return response;
            }

            response.IsSuccessful = false;

            return response;
        }
    }
}
