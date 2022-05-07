namespace ApiRequestLibrary
{
    using MobileRequestApiDTO;

    public class ResellerBalance : IResellerBalance
    {
        private readonly IWebRequestClient webRequestClient;

        private readonly IApiURLFactory apiUrlFactory;

        public ResellerBalance(IWebRequestClient webRequestClient, IApiURLFactory apiUrlFactory)
        {
            this.webRequestClient = webRequestClient;
            this.apiUrlFactory = apiUrlFactory;
        }

        async Task<bool> IResellerBalance.UpdateBalance(ResellerBalanceDTO resellerBalance)
        {
            try
            {
                await this.webRequestClient.PostAsync<ResellerBalanceDTO>(this.apiUrlFactory.GetURL(ApiEndPointType.resellerBalance), resellerBalance);
                return true;
            }
            catch
            {
            }

            return false;
        }
    }
}
