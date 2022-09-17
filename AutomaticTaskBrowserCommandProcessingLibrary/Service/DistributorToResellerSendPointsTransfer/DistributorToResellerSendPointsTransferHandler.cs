namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using AutomaticTaskSharedLibrary;

    using OpenQA.Selenium.Chrome;

    public class DistributorToResellerSendPointsTransferHandler : IDistributorToResellerSendPointsTransferHandler
    {
        private readonly IDistributorToResellerSendPointsTransferAdapter distributorToResellerSendPointsTransferAdapter;

        public DistributorToResellerSendPointsTransferHandler(IDistributorToResellerSendPointsTransferAdapter distributorToResellerSendPointsTransferAdapter)
        {
            this.distributorToResellerSendPointsTransferAdapter = distributorToResellerSendPointsTransferAdapter;
        }

        public AutomaticTaskType AutomaticTaskType => AutomaticTaskType.distributorToResellerSendPointsTransfer;

        public Task<bool> Execute(IAutomaticTask message)
        {
            var automaticTaskTransferPoints = (AutomaticTaskTransferPoints)message;
            return Task.Factory.StartNew(
                () =>
                    {
                        var invoiceLineItemId = automaticTaskTransferPoints.DistributorToResellerSendPointsTransferRequest.InvoiceLineItemId;
                        var softwareType = automaticTaskTransferPoints.DistributorToResellerSendPointsTransferRequest.SoftwareType;
                        var userId = automaticTaskTransferPoints.DistributorToResellerSendPointsTransferRequest.UserId;
                        var password = automaticTaskTransferPoints.DistributorToResellerSendPointsTransferRequest.Password;
                        var accountId = automaticTaskTransferPoints.DistributorToResellerSendPointsTransferRequest.AccountId;
                        var points = automaticTaskTransferPoints.DistributorToResellerSendPointsTransferRequest.Points;

                        var driver = new ChromeDriver(@"C:\Program Files (x86)");
                        try
                        {
                            var request = new DistributorToResellerSendPointsTransferRequest(invoiceLineItemId, softwareType, userId, password, accountId, points);
                            var response = this.distributorToResellerSendPointsTransferAdapter.Execute(driver, request);
                            // ZQ driver?.Quit();
                            return response.IsSuccessful;
                        }
                        catch (Exception e)
                        {
                        }

                        driver?.Quit();
                        return false;
                    });
        }
    }
}
