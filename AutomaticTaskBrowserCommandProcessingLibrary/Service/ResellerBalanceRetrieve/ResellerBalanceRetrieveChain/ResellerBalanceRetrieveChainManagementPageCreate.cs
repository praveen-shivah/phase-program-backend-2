namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public class ResellerBalanceRetrieveChainManagementPageCreate : IResellerBalanceRetrieveChain
    {
        private readonly IResellerBalanceRetrieveChain vendorBalanceRetrieveChain;

        public ResellerBalanceRetrieveChainManagementPageCreate(IResellerBalanceRetrieveChain vendorBalanceRetrieveChain)
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

            response.ResponseType = VendorBalanceRetrieveResponseType.managementCreate;
            try
            {
                response.ManagementPage = new RiverSweepsShopsManagement(driver);
            }
            catch
            {
                response.IsSuccessful = false;
            }

            return response;
        }
    }
}
