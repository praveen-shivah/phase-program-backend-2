namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public class ResellerBalanceRetrieveChainLoginSubmit : IResellerBalanceRetrieveChain
    {
        private readonly IResellerBalanceRetrieveChain resellerBalanceRetrieveChain;

        private readonly IManagementPageFactory managementPageFactory;

        public ResellerBalanceRetrieveChainLoginSubmit(IResellerBalanceRetrieveChain resellerBalanceRetrieveChain, IManagementPageFactory managementPageFactory)
        {
            this.resellerBalanceRetrieveChain = resellerBalanceRetrieveChain;
            this.managementPageFactory = managementPageFactory;
        }

        ResellerBalanceRetrieveResponse IResellerBalanceRetrieveChain.Execute(IWebDriver driver, ResellerBalanceRetrieveRequest resellerBalanceRetrieveRequest)
        {
            var response = this.resellerBalanceRetrieveChain.Execute(driver, resellerBalanceRetrieveRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            response.ResponseType = ResellerBalanceRetrieveResponseType.loginSubmit;

            response.IsSuccessful = response.LoginPage.Submit();
            if (response.IsSuccessful)
            {
                response.ManagementPage = this.managementPageFactory.Create(driver, resellerBalanceRetrieveRequest.SoftwareType);
            }

            return response;
        }
    }
}
