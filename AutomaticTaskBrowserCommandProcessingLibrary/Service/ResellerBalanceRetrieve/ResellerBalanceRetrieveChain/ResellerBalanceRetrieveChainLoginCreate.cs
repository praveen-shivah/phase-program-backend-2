namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public class ResellerBalanceRetrieveChainLoginCreate : IResellerBalanceRetrieveChain
    {
        private readonly IResellerBalanceRetrieveChain vendorBalanceRetrieveChain;

        private readonly ILoginPageFactory vendorToOperatorTransferLoginPageFactory;

        public ResellerBalanceRetrieveChainLoginCreate(IResellerBalanceRetrieveChain vendorBalanceRetrieveChain, ILoginPageFactory vendorToOperatorTransferLoginPageFactory)
        {
            this.vendorBalanceRetrieveChain = vendorBalanceRetrieveChain;
            this.vendorToOperatorTransferLoginPageFactory = vendorToOperatorTransferLoginPageFactory;
        }

        ResellerBalanceRetrieveResponse IResellerBalanceRetrieveChain.Execute(IWebDriver driver, ResellerBalanceRetrieveRequest vendorBalanceRetrieveRequest)
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
