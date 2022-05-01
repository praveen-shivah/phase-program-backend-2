namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using AutomaticTaskLibrary;

    using OpenQA.Selenium.Chrome;

    public class VendorToOperatorSendPointsTransferHandler : IVendorToOperatorSendPointsTransferHandler
    {
        private readonly IBrowserContextFactory browserContextFactory;

        public VendorToOperatorSendPointsTransferHandler(IBrowserContextFactory browserContextFactory)
        {
            this.browserContextFactory = browserContextFactory;
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
                            var loginPage = new RiverSweepsLogin(driver, userId, password);
                            loginPage.VerifyPageLoaded();
                            var riverSweepsShopsManagement = loginPage.Submit();
                            if (riverSweepsShopsManagement == null)
                            {
                                return false;
                            }

                            riverSweepsShopsManagement.MakeDeposit(accountId, points);
                        }
                        catch (Exception e)
                        {
                        }

                        // driver?.Quit();
                        return true;
                    });
        }
    }
}
