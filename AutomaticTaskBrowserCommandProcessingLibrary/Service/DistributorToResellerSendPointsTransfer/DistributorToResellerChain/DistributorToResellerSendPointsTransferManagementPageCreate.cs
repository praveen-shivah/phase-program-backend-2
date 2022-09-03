namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public class DistributorToResellerSendPointsTransferManagementPageCreate : IDistributorToResellerSendPointsTransferChain
    {
        private readonly IDistributorToResellerSendPointsTransferChain distributorToResellerSendPointsTransferChain;

        private readonly IManagementPageFactory managementPageFactory;

        public DistributorToResellerSendPointsTransferManagementPageCreate(
            IDistributorToResellerSendPointsTransferChain distributorToResellerSendPointsTransferChain,
            IManagementPageFactory managementPageFactory)
        {
            this.distributorToResellerSendPointsTransferChain = distributorToResellerSendPointsTransferChain;
            this.managementPageFactory = managementPageFactory;
        }

        DistributorToResellerTransferResponse IDistributorToResellerSendPointsTransferChain.Execute(IWebDriver driver, DistributorToResellerSendPointsTransferRequest distributorToResellerSendPointsTransferRequest)
        {
            var response = this.distributorToResellerSendPointsTransferChain.Execute(driver, distributorToResellerSendPointsTransferRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            response.ResponseType = DistributorToOperatorTransferResponseType.managementCreate;
            try
            {
                response.ManagementPage = this.managementPageFactory.Create(driver, distributorToResellerSendPointsTransferRequest.SoftwareType);
            }
            catch
            {
                response.IsSuccessful = false;
            }

            return response;
        }
    }
}
