namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public class DistributorToResellerSendPointsTransferManagementPageCreate : IDistributorToResellerSendPointsTransferChain
    {
        private readonly IDistributorToResellerSendPointsTransferChain vendorToOperatorSendPointsTransfer;

        public DistributorToResellerSendPointsTransferManagementPageCreate(IDistributorToResellerSendPointsTransferChain vendorToOperatorSendPointsTransfer)
        {
            this.vendorToOperatorSendPointsTransfer = vendorToOperatorSendPointsTransfer;
        }

        DistributorToResellerTransferResponse IDistributorToResellerSendPointsTransferChain.Execute(IWebDriver driver, DistributorToResellerSendPointsTransferRequest vendorToOperatorSendPointsTransferRequest)
        {
            var response = this.vendorToOperatorSendPointsTransfer.Execute(driver, vendorToOperatorSendPointsTransferRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            response.ResponseType = VendorToOperatorTransferResponseType.managementCreate;
            try
            {
                response.ManagementPage = new RiverSweepsShopsManagement(driver);
            }
            catch
            {
                response.IsSuccessful = false;
            }

            return response;
        }
    }
}
