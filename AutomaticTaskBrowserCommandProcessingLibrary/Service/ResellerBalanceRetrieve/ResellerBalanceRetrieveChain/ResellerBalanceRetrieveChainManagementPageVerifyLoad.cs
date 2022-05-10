namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public class ResellerBalanceRetrieveChainManagementPageVerifyLoad : IResellerBalanceRetrieveChain
    {
        private readonly IResellerBalanceRetrieveChain vendorBalanceRetrieveChain;

        public ResellerBalanceRetrieveChainManagementPageVerifyLoad(IResellerBalanceRetrieveChain vendorBalanceRetrieveChain)
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

            response.ResponseType = VendorBalanceRetrieveResponseType.managementVerifyLoad;
            if (response.ManagementPage.IsPageUrlSet() && response.ManagementPage.VerifyPageLoaded())
            {
                return response;
            }

            response.IsSuccessful = false;

            return response;
        }
    }
}
