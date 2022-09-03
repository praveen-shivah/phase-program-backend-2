namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public class DistributorToResellerSendPointsTransferLoginCreate : IDistributorToResellerSendPointsTransferChain
    {
        private readonly IDistributorToResellerSendPointsTransferChain distributorToResellerSendPointsTransferChain;

        private readonly ILoginPageFactory loginPageFactory;

        public DistributorToResellerSendPointsTransferLoginCreate(IDistributorToResellerSendPointsTransferChain distributorToResellerSendPointsTransferChain, 
                                                             ILoginPageFactory loginPageFactory)
        {
            this.distributorToResellerSendPointsTransferChain = distributorToResellerSendPointsTransferChain;
            this.loginPageFactory = loginPageFactory;
        }

        DistributorToResellerTransferResponse IDistributorToResellerSendPointsTransferChain.Execute(IWebDriver driver, DistributorToResellerSendPointsTransferRequest distributorToResellerSendPointsTransferRequest)
        {
            var response = this.distributorToResellerSendPointsTransferChain.Execute(driver, distributorToResellerSendPointsTransferRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            response.ResponseType = DistributorToOperatorTransferResponseType.loginCreate;

            try
            {
                response.LoginPage = this.loginPageFactory.Create(driver, distributorToResellerSendPointsTransferRequest.LoginPageInformation);
            }
            catch
            {
                response.IsSuccessful = false;
            }

            return response;
        }
    }
}
