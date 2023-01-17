namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public class ResellerBalanceRetrieveManagementGetBalance : IResellerBalanceRetrieveChain
    {
        private readonly IResellerBalanceRetrieveChain resellerBalanceRetrieveChain;

        public ResellerBalanceRetrieveManagementGetBalance(IResellerBalanceRetrieveChain resellerBalanceRetrieveChain)
        {
            this.resellerBalanceRetrieveChain = resellerBalanceRetrieveChain;
        }

        ResellerBalanceRetrieveResponse IResellerBalanceRetrieveChain.Execute(IWebDriver driver, ResellerBalanceRetrieveRequest resellerBalanceRetrieveRequest)
        {
            var response = this.resellerBalanceRetrieveChain.Execute(driver, resellerBalanceRetrieveRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            response.ResponseType = ResellerBalanceRetrieveResponseType.managementRetrieveBalance;

            response.ResellerBalance = response.ManagementPage.GetBalance();
            response.BalanceAsPoints = (int)(double.Parse(response.ResellerBalance) * 100);

            return response;
        }
    }
}
