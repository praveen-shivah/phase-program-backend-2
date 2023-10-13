namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using Microsoft.VisualBasic;
    using OpenQA.Selenium;
    
    public class ResellerPlayersRetrieveChainSavePlayer : IResellerPlayersRetrieveChain
    {
        private readonly IResellerPlayersRetrieveChain resellerPlayersRetrieveChain;
        private readonly IResellerPlayerPageFactory playersReportsPageFactory;

        public ResellerPlayersRetrieveChainSavePlayer(IResellerPlayersRetrieveChain resellerPlayersRetrieveChain, IResellerPlayerPageFactory playersReportsPageFactory)
        {
            this.resellerPlayersRetrieveChain = resellerPlayersRetrieveChain;
            this.playersReportsPageFactory = playersReportsPageFactory;

        }
        ResellerPlayersRetrieveResponse IResellerPlayersRetrieveChain.Execute(IWebDriver driver, ResellerPlayersRetrieveRequest request)
        {
            var response = this.resellerPlayersRetrieveChain.Execute(driver, request);
            if (!response.IsSuccessful)
            {
                return response;
            }

            response.ResponseType = ResellerPlayersRetrieveResponseType.playersReportCreate;

            try
            {
                response.Details = response.PlayersReportsPage.SavePlayersDetails(request);
            }
            catch
            {
                response.IsSuccessful = false;
            }

            return response;
        }
    }
}
