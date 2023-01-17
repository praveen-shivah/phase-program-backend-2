namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public class ResellerBalanceRetrieveChainLogoutVerifyLoad : IResellerBalanceRetrieveChain
    {
        private readonly IResellerBalanceRetrieveChain resellerBalanceRetrieveChain;

        public ResellerBalanceRetrieveChainLogoutVerifyLoad(IResellerBalanceRetrieveChain resellerBalanceRetrieveChain)
        {
            this.resellerBalanceRetrieveChain = resellerBalanceRetrieveChain;
        }

        ResellerBalanceRetrieveResponse IResellerBalanceRetrieveChain.Execute(IWebDriver driver, ResellerBalanceRetrieveRequest resellerBalanceRetrieveRequest)
        {
            var response = this.resellerBalanceRetrieveChain.Execute(driver, resellerBalanceRetrieveRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            response.LogoutPage.Logout();
            response.ResponseType = ResellerBalanceRetrieveResponseType.logoutVerifyLoad;
            response.LogoutPage.VerifyPageUrl();

            return response;
        }
    }
}
