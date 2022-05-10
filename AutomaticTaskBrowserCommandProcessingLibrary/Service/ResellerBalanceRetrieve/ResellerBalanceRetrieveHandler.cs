namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using AutomaticTaskLibrary;

    using OpenQA.Selenium.Chrome;

    public class ResellerBalanceRetrieveHandler : IResellerBalanceRetrieveHandler
    {
        private readonly IResellerBalanceRetrieveAdapter vendorBalanceRetrieveAdapter;

        public ResellerBalanceRetrieveHandler(IResellerBalanceRetrieveAdapter vendorBalanceRetrieveAdapter)
        {
            this.vendorBalanceRetrieveAdapter = vendorBalanceRetrieveAdapter;
        }

        public AutomaticTaskType AutomaticTaskType => AutomaticTaskType.distributorToResellerSendPointsTransfer;

        public Task<bool> Execute(IAutomaticTask message)
        {
            var automaticTask = (AutomaticTaskResellerBalanceRetrieve)message;
            return Task.Factory.StartNew(
                () =>
                    {
                        var resellerId = automaticTask.VendorBalanceRetrieveRequest.ResellerId;
                        var softwareType = automaticTask.VendorBalanceRetrieveRequest.SoftwareType;
                        var userId = automaticTask.VendorBalanceRetrieveRequest.UserId;
                        var password = automaticTask.VendorBalanceRetrieveRequest.Password;

                        var driver = new ChromeDriver(@"C:\Program Files (x86)");
                        try
                        {
                            var request = new ResellerBalanceRetrieveRequest(softwareType, resellerId, userId, password);
                            var response = this.vendorBalanceRetrieveAdapter.Execute(driver, request);
                            driver.Quit();
                            return response.IsSuccessful;
                        }
                        catch
                        {
                        }

                        driver.Quit();
                        return false;
                    });
        }
    }
}
