namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public class ResellerBalanceRetrieveChainLoginSubmit : IResellerBalanceRetrieveChain
    {
        private readonly IResellerBalanceRetrieveChain vendorBalanceRetrieveChain;

        public ResellerBalanceRetrieveChainLoginSubmit(IResellerBalanceRetrieveChain vendorBalanceRetrieveChain)
        {
            this.vendorBalanceRetrieveChain = vendorBalanceRetrieveChain;
        }

        ResellerBalanceRetrieveResponse IResellerBalanceRetrieveChain.Execute(IWebDriver driver, ResellerBalanceRetrieveRequest resellerBalanceRetrieveRequest)
        {
            var response = this.vendorBalanceRetrieveChain.Execute(driver, resellerBalanceRetrieveRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            response.ResponseType = VendorBalanceRetrieveResponseType.loginSubmit;

            response.IsSuccessful = response.LoginPage.Submit();
            if (response.IsSuccessful)
            {
                response.ManagementPage = new RiverSweepsShopsManagement(driver);
            }

            return response;
        }
    }
}
