namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;
    
    public class ResellerPlayersRetrieveChainLogoutVerifyLoad : IResellerPlayersRetrieveChain
    {
        private readonly IResellerPlayersRetrieveChain resellerPlayersRetrieveChain;
        public ResellerPlayersRetrieveChainLogoutVerifyLoad(IResellerPlayersRetrieveChain resellerPlayersRetrieveChain)
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

            response.LogoutPage.Logout();
            response.ResponseType = ResellerPlayersRetrieveResponseType.logoutVerifyLoad;
            response.LogoutPage.VerifyPageUrl();

            return response;
        }
    }
}
