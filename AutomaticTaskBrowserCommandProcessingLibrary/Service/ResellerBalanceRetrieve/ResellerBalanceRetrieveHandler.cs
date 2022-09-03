namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using AutomaticTaskSharedLibrary;

    using OpenQA.Selenium.Chrome;

    public class ResellerBalanceRetrieveHandler : IResellerBalanceRetrieveHandler
    {
        private readonly IResellerBalanceRetrieveAdapter resellerBalanceRetrieveAdapter;

        public ResellerBalanceRetrieveHandler(IResellerBalanceRetrieveAdapter resellerBalanceRetrieveAdapter)
        {
            this.resellerBalanceRetrieveAdapter = resellerBalanceRetrieveAdapter;
        }

        public AutomaticTaskType AutomaticTaskType => AutomaticTaskType.distributorToResellerSendPointsTransfer;

        public Task<bool> Execute(IAutomaticTask message)
        {
            var automaticTask = (AutomaticTaskResellerBalanceRetrieve)message;
            return Task.Factory.StartNew(
                () =>
                    {
                        var resellerId = automaticTask.ResellerBalanceRetrieveRequest.ResellerId;
                        var softwareType = automaticTask.ResellerBalanceRetrieveRequest.SoftwareType;
                        var userId = automaticTask.ResellerBalanceRetrieveRequest.UserId;
                        var password = automaticTask.ResellerBalanceRetrieveRequest.Password;
                        var organizationId = automaticTask.OrganizationId;
                        var apiKey = automaticTask.APIKey;

                        var driver = new ChromeDriver(@"C:\Program Files (x86)");
                        try
                        {
                            var request = new ResellerBalanceRetrieveRequest(softwareType, organizationId, apiKey, resellerId, userId, password);
                            var response = this.resellerBalanceRetrieveAdapter.Execute(driver, request);
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