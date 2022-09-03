namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public class ResellerBalanceRetrieveChainLogoutCreate : IResellerBalanceRetrieveChain
    {
        private readonly IResellerBalanceRetrieveChain resellerBalanceRetrieveChain;

        private readonly ILogoutPageFactory logoutPageFactory;

        public ResellerBalanceRetrieveChainLogoutCreate(IResellerBalanceRetrieveChain resellerBalanceRetrieveChain, ILogoutPageFactory logoutPageFactory)
        {
            this.resellerBalanceRetrieveChain = resellerBalanceRetrieveChain;
            this.logoutPageFactory = logoutPageFactory;
        }

        ResellerBalanceRetrieveResponse IResellerBalanceRetrieveChain.Execute(IWebDriver driver, ResellerBalanceRetrieveRequest resellerBalanceRetrieveRequest)
        {
            var response = this.resellerBalanceRetrieveChain.Execute(driver, resellerBalanceRetrieveRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            response.ResponseType = ResellerBalanceRetrieveResponseType.logoutCreate;

            try
            {
                response.LogoutPage = this.logoutPageFactory.Create(driver, resellerBalanceRetrieveRequest.LoginPageInformation.SoftwareType);
            }
            catch
            {
                response.IsSuccessful = false;
            }

            return response;
        }
    }
}
