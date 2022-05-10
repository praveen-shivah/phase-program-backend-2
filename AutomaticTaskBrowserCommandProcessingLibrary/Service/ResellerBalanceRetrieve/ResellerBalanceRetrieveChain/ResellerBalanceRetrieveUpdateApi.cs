namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using ApiRequestLibrary;

    using MobileRequestApiDTO;

    using OpenQA.Selenium;

    public class ResellerBalanceRetrieveUpdateApi : IResellerBalanceRetrieveChain
    {
        private readonly IResellerBalanceRetrieveChain vendorBalanceRetrieveChain;

        private readonly IResellerBalance resellerBalance;

        public ResellerBalanceRetrieveUpdateApi(IResellerBalanceRetrieveChain vendorBalanceRetrieveChain, IResellerBalance resellerBalance)
        {
            this.vendorBalanceRetrieveChain = vendorBalanceRetrieveChain;
            this.resellerBalance = resellerBalance;
        }

        ResellerBalanceRetrieveResponse IResellerBalanceRetrieveChain.Execute(IWebDriver driver, ResellerBalanceRetrieveRequest resellerBalanceRetrieveRequest)
        {
            var response = this.vendorBalanceRetrieveChain.Execute(driver, resellerBalanceRetrieveRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            response.ResponseType = VendorBalanceRetrieveResponseType.apiStore;
            response.ResellerBalance = response.ManagementPage.GetBalance();

            this.resellerBalance.UpdateBalance(
                new ResellerBalanceDTO()
                {
                    Balance = response.ResellerBalance,
                    ResellerId = resellerBalanceRetrieveRequest.ResellerId
                });

            return response;
        }
    }
}
