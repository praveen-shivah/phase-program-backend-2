namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public class VendorBalanceRetrieveChainLoginCreate : IVendorBalanceRetrieveChain
    {
        private readonly IVendorBalanceRetrieveChain vendorBalanceRetrieveChain;

        private readonly ILoginPageFactory vendorToOperatorTransferLoginPageFactory;

        public VendorBalanceRetrieveChainLoginCreate(IVendorBalanceRetrieveChain vendorBalanceRetrieveChain, ILoginPageFactory vendorToOperatorTransferLoginPageFactory)
        {
            this.vendorBalanceRetrieveChain = vendorBalanceRetrieveChain;
            this.vendorToOperatorTransferLoginPageFactory = vendorToOperatorTransferLoginPageFactory;
        }

        VendorBalanceRetrieveResponse IVendorBalanceRetrieveChain.Execute(IWebDriver driver, VendorBalanceRetrieveRequest vendorBalanceRetrieveRequest)
        {
            var response = this.vendorBalanceRetrieveChain.Execute(driver, vendorBalanceRetrieveRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            response.ResponseType = VendorBalanceRetrieveResponseType.loginCreate;

            try
            {
                response.LoginPage = this.vendorToOperatorTransferLoginPageFactory.Create(driver, vendorBalanceRetrieveRequest.LoginPageInformation);
            }
            catch
            {
                response.IsSuccessful = false;
            }

            return response;
        }
    }
}
