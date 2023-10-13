namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;
    
    public class ResellerPlayersRetrieveChainLogoutCreate : IResellerPlayersRetrieveChain
    {
        private readonly IResellerPlayersRetrieveChain resellerPlayersRetrieveChain;

        private readonly ILogoutPageFactory logoutPageFactory;
        public ResellerPlayersRetrieveChainLogoutCreate(IResellerPlayersRetrieveChain resellerPlayersRetrieveChain, ILogoutPageFactory logoutPageFactory)
        {
            this.resellerPlayersRetrieveChain = resellerPlayersRetrieveChain;
            this.logoutPageFactory = logoutPageFactory;
        }
        ResellerPlayersRetrieveResponse IResellerPlayersRetrieveChain.Execute(IWebDriver driver, ResellerPlayersRetrieveRequest resellerPlayersRetrieveRequest)
        {
            var response = this.resellerPlayersRetrieveChain.Execute(driver, resellerPlayersRetrieveRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            response.ResponseType = ResellerPlayersRetrieveResponseType.logoutCreate;

            try
            {
                response.LogoutPage = this.logoutPageFactory.Create(driver, resellerPlayersRetrieveRequest.LoginPageInformation.SoftwareType);
            }
            catch
            {
                response.IsSuccessful = false;
            }

            return response;
        }
    }
}
