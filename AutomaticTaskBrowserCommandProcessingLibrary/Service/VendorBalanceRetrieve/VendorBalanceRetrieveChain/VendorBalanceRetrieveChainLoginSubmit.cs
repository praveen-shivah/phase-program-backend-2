namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public class VendorBalanceRetrieveChainLoginSubmit : IVendorBalanceRetrieveChain
    {
        private readonly IVendorBalanceRetrieveChain vendorBalanceRetrieveChain;

        public VendorBalanceRetrieveChainLoginSubmit(IVendorBalanceRetrieveChain vendorBalanceRetrieveChain)
        {
            this.vendorBalanceRetrieveChain = vendorBalanceRetrieveChain;
        }

        VendorBalanceRetrieveResponse IVendorBalanceRetrieveChain.Execute(IWebDriver driver, VendorBalanceRetrieveRequest vendorBalanceRetrieveRequest)
        {
            var response = this.vendorBalanceRetrieveChain.Execute(driver, vendorBalanceRetrieveRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            response.ResponseType = VendorBalanceRetrieveResponseType.loginSubmit;

            response.ManagementPage = response.LoginPage.Submit();
            if (response.ManagementPage == null)
            {
                response.IsSuccessful = false;
            }

            return response;
        }
    }
}
