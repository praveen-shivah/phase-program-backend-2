namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using AutomaticTaskLibrary;

    using OpenQA.Selenium.Chrome;

    public class VendorBalanceRetrieveHandler : IVendorBalanceRetrieveHandler
    {
        private readonly IVendorBalanceRetrieveAdapter vendorBalanceRetrieveAdapter;

        public VendorBalanceRetrieveHandler(IVendorBalanceRetrieveAdapter vendorBalanceRetrieveAdapter)
        {
            this.vendorBalanceRetrieveAdapter = vendorBalanceRetrieveAdapter;
        }

        public AutomaticTaskType AutomaticTaskType => AutomaticTaskType.vendorToOperatorSendPointsTransfer;

        public Task<bool> Execute(IAutomaticTask message)
        {
            var automaticTask = (AutomaticTaskVendorBalanceRetrieve)message;
            return Task.Factory.StartNew(
                () =>
                    {
                        var softwareType = automaticTask.VendorBalanceRetrieveRequest.SoftwareType;
                        var userId = automaticTask.VendorBalanceRetrieveRequest.UserId;
                        var password = automaticTask.VendorBalanceRetrieveRequest.Password;

                        var driver = new ChromeDriver(@"C:\Program Files (x86)");
                        try
                        {
                            var request = new VendorBalanceRetrieveRequest(softwareType, userId, password);
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
