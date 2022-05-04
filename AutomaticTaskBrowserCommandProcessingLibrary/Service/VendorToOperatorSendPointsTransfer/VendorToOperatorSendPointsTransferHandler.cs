namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using AutomaticTaskLibrary;

    using OpenQA.Selenium.Chrome;

    public class VendorToOperatorSendPointsTransferHandler : IVendorToOperatorSendPointsTransferHandler
    {
        private readonly IVendorToOperatorSendPointsTransferAdapter vendorToOperatorSendPointsTransferAdapter;

        public VendorToOperatorSendPointsTransferHandler(IVendorToOperatorSendPointsTransferAdapter vendorToOperatorSendPointsTransferAdapter)
        {
            this.vendorToOperatorSendPointsTransferAdapter = vendorToOperatorSendPointsTransferAdapter;
        }

        public AutomaticTaskType AutomaticTaskType => AutomaticTaskType.vendorToOperatorSendPointsTransfer;

        public Task<bool> Execute(IAutomaticTask message)
        {
            var automaticTaskTransferPoints = (AutomaticTaskTransferPoints)message;
            return Task.Factory.StartNew(
                () =>
                    {
                        var softwareType = automaticTaskTransferPoints.VendorToOperatorSendPointsTransferRequest.SoftwareType;
                        var userId = automaticTaskTransferPoints.VendorToOperatorSendPointsTransferRequest.UserId;
                        var password = automaticTaskTransferPoints.VendorToOperatorSendPointsTransferRequest.Password;
                        var accountId = automaticTaskTransferPoints.VendorToOperatorSendPointsTransferRequest.AccountId;
                        var points = automaticTaskTransferPoints.VendorToOperatorSendPointsTransferRequest.Points;

                        var driver = new ChromeDriver(@"C:\Program Files (x86)");
                        try
                        {
                            var request = new VendorToOperatorSendPointsTransferRequest(softwareType, userId, password, accountId, points);
                            var response = this.vendorToOperatorSendPointsTransferAdapter.Execute(driver, request);
                            driver?.Quit();
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
