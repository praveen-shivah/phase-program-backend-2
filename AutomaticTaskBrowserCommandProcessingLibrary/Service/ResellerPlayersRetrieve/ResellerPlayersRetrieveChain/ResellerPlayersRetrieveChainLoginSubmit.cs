namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;
    
    public class ResellerPlayersRetrieveChainLoginSubmit:IResellerPlayersRetrieveChain
    {

        private readonly IResellerPlayersRetrieveChain resellerPlayersRetrieveChain;

        private readonly IResellerPlayerPageFactory playersReportsPageFactory;
        public ResellerPlayersRetrieveChainLoginSubmit(IResellerPlayersRetrieveChain resellerPlayersRetrieveChain, IResellerPlayerPageFactory playersReportsPageFactory)
        {
            this.resellerPlayersRetrieveChain = resellerPlayersRetrieveChain;
            this.playersReportsPageFactory = playersReportsPageFactory;
        }
        ResellerPlayersRetrieveResponse IResellerPlayersRetrieveChain.Execute(IWebDriver driver, ResellerPlayersRetrieveRequest resellerplayersRetrieveRequest)
        {
            var response = this.resellerPlayersRetrieveChain.Execute(driver, resellerplayersRetrieveRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            response.ResponseType = ResellerPlayersRetrieveResponseType.loginSubmit;

            response.IsSuccessful = response.LoginPage.Submit();
            if (response.IsSuccessful)
            {
                response.PlayersReportsPage = this.playersReportsPageFactory.Create(driver, resellerplayersRetrieveRequest.SoftwareType);
            }

            return response;
        }
    }
}
