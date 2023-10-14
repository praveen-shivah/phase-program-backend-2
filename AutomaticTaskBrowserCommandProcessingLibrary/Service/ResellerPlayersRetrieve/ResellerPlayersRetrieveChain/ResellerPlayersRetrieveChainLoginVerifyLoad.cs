namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;
    
    public class ResellerPlayersRetrieveChainLoginVerifyLoad : IResellerPlayersRetrieveChain
    {
        private readonly IResellerPlayersRetrieveChain resellerPlayersRetrieveChain;
        public ResellerPlayersRetrieveChainLoginVerifyLoad(IResellerPlayersRetrieveChain resellerPlayersRetrieveChain)
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

            response.ResponseType = ResellerPlayersRetrieveResponseType.loginVerifyLoad;
            response.LoginPage.VerifyPageLoaded();

            return response;
        }
    }
}
