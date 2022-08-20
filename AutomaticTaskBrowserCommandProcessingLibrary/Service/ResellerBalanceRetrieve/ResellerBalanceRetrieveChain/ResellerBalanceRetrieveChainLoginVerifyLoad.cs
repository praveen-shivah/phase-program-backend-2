namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public class ResellerBalanceRetrieveChainLoginVerifyLoad : IResellerBalanceRetrieveChain
    {
        private readonly IResellerBalanceRetrieveChain resellerBalanceRetrieveChain;

        public ResellerBalanceRetrieveChainLoginVerifyLoad(IResellerBalanceRetrieveChain resellerBalanceRetrieveChain)
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

            response.ResponseType = ResellerBalanceRetrieveResponseType.loginVerifyLoad;
            response.LoginPage.VerifyPageLoaded();

            return response;
        }
    }
}
