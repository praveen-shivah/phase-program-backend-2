namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public class DistributorToResellerSendPointsTransferLogoutCreate : IDistributorToResellerSendPointsTransferChain
    {
        private readonly IDistributorToResellerSendPointsTransferChain distributorToResellerSendPointsTransferChain;

        private readonly ILogoutPageFactory logoutPageFactory;

        public DistributorToResellerSendPointsTransferLogoutCreate(IDistributorToResellerSendPointsTransferChain distributorToResellerSendPointsTransferChain, 
                                                                   ILogoutPageFactory logoutPageFactory)
        {
            this.distributorToResellerSendPointsTransferChain = distributorToResellerSendPointsTransferChain;
            this.logoutPageFactory = logoutPageFactory;
        }

        DistributorToResellerTransferResponse IDistributorToResellerSendPointsTransferChain.Execute(IWebDriver driver, DistributorToResellerSendPointsTransferRequest distributorToResellerSendPointsTransferRequest)
        {
            var response = this.distributorToResellerSendPointsTransferChain.Execute(driver, distributorToResellerSendPointsTransferRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            response.ResponseType = DistributorToOperatorTransferResponseType.logoutCreate;

            try
            {
                response.LogoutPage = this.logoutPageFactory.Create(driver, distributorToResellerSendPointsTransferRequest.LoginPageInformation.SoftwareType);
            }
            catch
            {
                response.IsSuccessful = false;
            }

            return response;
        }
    }
}
