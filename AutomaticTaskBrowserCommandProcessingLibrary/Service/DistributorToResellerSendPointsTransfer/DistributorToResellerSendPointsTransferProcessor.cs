namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using AutomaticTaskSharedLibrary;

    using OpenQA.Selenium.Chrome;

    public class DistributorToResellerSendPointsTransferProcessor : IDistributorToResellerSendPointsTransferProcessor
    {
        private readonly IDistributorToResellerSendPointsTransferAdapter distributorToResellerSendPointsTransferAdapter;

        public DistributorToResellerSendPointsTransferProcessor(IDistributorToResellerSendPointsTransferAdapter distributorToResellerSendPointsTransferAdapter)
        {
            this.distributorToResellerSendPointsTransferAdapter = distributorToResellerSendPointsTransferAdapter;
        }

        Task<bool> IDistributorToResellerSendPointsTransferProcessor.Execute(DistributorToResellerSendPointsTransferRequestDto distributorToResellerSendPointsTransferRequestDto)
        {
            return Task.Factory.StartNew(
                () =>
                    {
                        var invoiceLineItemId = distributorToResellerSendPointsTransferRequestDto.InvoiceLineItemId;
                        var softwareType = distributorToResellerSendPointsTransferRequestDto.SoftwareType;
                        var userId = distributorToResellerSendPointsTransferRequestDto.UserId;
                        var password = distributorToResellerSendPointsTransferRequestDto.Password;
                        var accountId = distributorToResellerSendPointsTransferRequestDto.AccountId;
                        var points = distributorToResellerSendPointsTransferRequestDto.Points;

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
