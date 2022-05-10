namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public class DistributorToResellerSendPointsTransferManagementMakeDeposit : IDistributorToResellerSendPointsTransferChain
    {
        private readonly IDistributorToResellerSendPointsTransferChain vendorToOperatorSendPointsTransferChain;

        public DistributorToResellerSendPointsTransferManagementMakeDeposit(IDistributorToResellerSendPointsTransferChain vendorToOperatorSendPointsTransferChain)
        {
            this.vendorToOperatorSendPointsTransferChain = vendorToOperatorSendPointsTransferChain;
        }

        DistributorToResellerTransferResponse IDistributorToResellerSendPointsTransferChain.Execute(IWebDriver driver, DistributorToResellerSendPointsTransferRequest vendorToOperatorSendPointsTransferRequest)
        {
            var response = this.vendorToOperatorSendPointsTransferChain.Execute(driver, vendorToOperatorSendPointsTransferRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            response.ResponseType = VendorToOperatorTransferResponseType.managementMakeDeposit;

            // We'll consider it successful if we get this far so as not to duplicate deposits.
            // Any failure up to this point and we can do a retry.
            response.ManagementPage.MakeDeposit(vendorToOperatorSendPointsTransferRequest.Points);

            return response;
        }
    }
}
