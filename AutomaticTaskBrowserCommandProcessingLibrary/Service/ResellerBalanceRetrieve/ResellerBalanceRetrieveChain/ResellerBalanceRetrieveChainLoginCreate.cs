namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public class ResellerBalanceRetrieveChainLoginCreate : IResellerBalanceRetrieveChain
    {
        private readonly IResellerBalanceRetrieveChain resellerBalanceRetrieveChain;

        private readonly ILoginPageFactory loginPageFactory;

        public ResellerBalanceRetrieveChainLoginCreate(IResellerBalanceRetrieveChain resellerBalanceRetrieveChain, ILoginPageFactory loginPageFactory)
        {
            this.resellerBalanceRetrieveChain = resellerBalanceRetrieveChain;
            this.loginPageFactory = loginPageFactory;
        }

        ResellerBalanceRetrieveResponse IResellerBalanceRetrieveChain.Execute(IWebDriver driver, ResellerBalanceRetrieveRequest resellerBalanceRetrieveRequest)
        {
            var response = this.resellerBalanceRetrieveChain.Execute(driver, resellerBalanceRetrieveRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            response.ResponseType = ResellerBalanceRetrieveResponseType.loginCreate;

            try
            {
                response.LoginPage = this.loginPageFactory.Create(driver, resellerBalanceRetrieveRequest.LoginPageInformation);
            }
            catch
            {
                response.IsSuccessful = false;
            }

            return response;
        }
    }
}
