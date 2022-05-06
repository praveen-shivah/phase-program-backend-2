namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public class DistributorToResellerSendPointsTransferLoginCreate : IDistributorToResellerSendPointsTransferChain
    {
        private readonly IDistributorToResellerSendPointsTransferChain vendorToOperatorSendPointsTransferChain;

        private readonly ILoginPageFactory loginPageFactory;

        public DistributorToResellerSendPointsTransferLoginCreate(IDistributorToResellerSendPointsTransferChain vendorToOperatorSendPointsTransferChain, 
                                                             ILoginPageFactory loginPageFactory)
        {
            this.vendorToOperatorSendPointsTransferChain = vendorToOperatorSendPointsTransferChain;
            this.loginPageFactory = loginPageFactory;
        }

        DistributorToResellerTransferResponse IDistributorToResellerSendPointsTransferChain.Execute(IWebDriver driver, DistributorToResellerSendPointsTransferRequest vendorToOperatorSendPointsTransferRequest)
        {
            var response = this.vendorToOperatorSendPointsTransferChain.Execute(driver, vendorToOperatorSendPointsTransferRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            response.ResponseType = VendorToOperatorTransferResponseType.loginCreate;

            try
            {
                response.LoginPage = this.loginPageFactory.Create(driver, vendorToOperatorSendPointsTransferRequest.LoginPageInformation);
            }
            catch
            {
                response.IsSuccessful = false;
            }

            return response;
        }
    }
}
