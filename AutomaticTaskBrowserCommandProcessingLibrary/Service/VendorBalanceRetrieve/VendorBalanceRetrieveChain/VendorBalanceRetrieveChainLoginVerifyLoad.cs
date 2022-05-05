namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public class VendorBalanceRetrieveChainLoginVerifyLoad : IVendorBalanceRetrieveChain
    {
        private readonly IVendorBalanceRetrieveChain vendorBalanceRetrieveChain;

        public VendorBalanceRetrieveChainLoginVerifyLoad(IVendorBalanceRetrieveChain vendorBalanceRetrieveChain)
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

            response.ResponseType = VendorBalanceRetrieveResponseType.loginVerifyLoad;
            response.LoginPage.VerifyPageLoaded();

            return response;
        }
    }
}
