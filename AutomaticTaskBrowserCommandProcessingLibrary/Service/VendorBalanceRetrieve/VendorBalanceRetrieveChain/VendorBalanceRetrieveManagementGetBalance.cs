namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public class VendorBalanceRetrieveManagementGetBalance : IVendorBalanceRetrieveChain
    {
        private readonly IVendorBalanceRetrieveChain vendorBalanceRetrieveChain;

        public VendorBalanceRetrieveManagementGetBalance(IVendorBalanceRetrieveChain vendorBalanceRetrieveChain)
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

            response.ResponseType = VendorBalanceRetrieveResponseType.managementRetrieveBalance;

            response.VendorBalance = response.ManagementPage.GetBalance();

            return response;
        }
    }
}
