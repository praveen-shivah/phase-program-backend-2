namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;
    
    public class ResellerPlayersRetrieveChainLoginCreate:IResellerPlayersRetrieveChain
    {
        private readonly IResellerPlayersRetrieveChain resellerPlayersRetrieveChain;

        private readonly ILoginPageFactory loginPageFactory;
        public ResellerPlayersRetrieveChainLoginCreate(IResellerPlayersRetrieveChain resellerPlayersRetrieveChain, ILoginPageFactory loginPageFactory)
        {
            this.resellerPlayersRetrieveChain = resellerPlayersRetrieveChain;
            this.loginPageFactory = loginPageFactory;
        }

        ResellerPlayersRetrieveResponse IResellerPlayersRetrieveChain.Execute(IWebDriver driver, ResellerPlayersRetrieveRequest resellerPlayersRetrieveRequest)
        {
            var response = this.resellerPlayersRetrieveChain.Execute(driver, resellerPlayersRetrieveRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            response.ResponseType = ResellerPlayersRetrieveResponseType.loginCreate;

            try
            {
                response.LoginPage = this.loginPageFactory.Create(driver, resellerPlayersRetrieveRequest.LoginPageInformation);
            }
            catch
            {
                response.IsSuccessful = false;
            }

            return response;
        }
    }
}
