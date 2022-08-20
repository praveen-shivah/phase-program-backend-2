namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public class DistributorToResellerSendPointsTransferLoginVerifyLoad : IDistributorToResellerSendPointsTransferChain
    {
        private readonly IDistributorToResellerSendPointsTransferChain distributorToResellerSendPointsTransferChain;

        public DistributorToResellerSendPointsTransferLoginVerifyLoad(IDistributorToResellerSendPointsTransferChain distributorToResellerSendPointsTransferChain)
        {
            this.distributorToResellerSendPointsTransferChain = distributorToResellerSendPointsTransferChain;
        }

        DistributorToResellerTransferResponse IDistributorToResellerSendPointsTransferChain.Execute(IWebDriver driver, DistributorToResellerSendPointsTransferRequest distributorToResellerSendPointsTransferRequest)
        {
            var response = this.distributorToResellerSendPointsTransferChain.Execute(driver, distributorToResellerSendPointsTransferRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            response.ResponseType = DistributorToOperatorTransferResponseType.loginVerifyLoad;
            response.LoginPage.VerifyPageLoaded();

            return response;
        }
    }
}
