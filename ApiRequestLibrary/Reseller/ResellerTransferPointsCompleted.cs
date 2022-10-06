namespace ApiRequestLibrary;

using ApiDTO;

public class ResellerTransferPointsCompleted : IResellerTransferPointsCompleted
{
    private readonly IWebRequestClient webRequestClient;

    private readonly IApiURLFactory apiUrlFactory;

    public ResellerTransferPointsCompleted(IWebRequestClient webRequestClient, IApiURLFactory apiUrlFactory)
    {
        this.webRequestClient = webRequestClient;
        this.apiUrlFactory = apiUrlFactory;
    }

    async Task<bool> IResellerTransferPointsCompleted.MarkAsCompleted(ResellerTransferPointsCompletedDto resellerTransferPointsCompleted)
    {
        try
        {
            await this.webRequestClient.PostAsync<ResellerTransferPointsCompletedDto>(this.apiUrlFactory.GetBaseURL(), this.apiUrlFactory.GetURL(ApiEndPointType.resellerTransferPointsCompleted), resellerTransferPointsCompleted);
            return true;
        }
        catch
        {
        }

        return false;
    }
}