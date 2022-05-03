namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using AutomaticTaskLibrary;

    using OpenQA.Selenium.Chrome;

    public class VendorToOperatorSendPointsTransferHandler : IVendorToOperatorSendPointsTransferHandler
    {
        private readonly IBrowserContextFactory browserContextFactory;

        private readonly IVendorToOperatorSendPointsTransferFactory vendorToOperatorSendPointsTransferFactory;

        public VendorToOperatorSendPointsTransferHandler(IBrowserContextFactory browserContextFactory, IVendorToOperatorSendPointsTransferFactory vendorToOperatorSendPointsTransferFactory)
        {
            this.browserContextFactory = browserContextFactory;
            this.vendorToOperatorSendPointsTransferFactory = vendorToOperatorSendPointsTransferFactory;
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
                            var adapter = this.vendorToOperatorSendPointsTransferFactory.Create(softwareType);
                            var request = new VendorToOperatorSendPointsTransferRequest(userId, password, accountId, points);
                            var response = adapter.Execute(driver, request);
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
