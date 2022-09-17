namespace ApiRequestLibrary
{
    public class ApiURLFactory : IApiURLFactory
    {
        private string apiUrlBase = "http://localhost";

        string IApiURLFactory.GetBaseURL()
        {
            return this.apiUrlBase;
        }

        string IApiURLFactory.GetURL(ApiEndPointType apiEndPointType)
        {
            switch (apiEndPointType)
            {
                case ApiEndPointType.resellerBalance: return $"api/reseller/reseller-balance";
                case ApiEndPointType.resellerTransferPointsCompleted: return $"api/reseller/reseller-transfer-points-completed";
                default:
                    throw new ArgumentOutOfRangeException(nameof(apiEndPointType), apiEndPointType, null);
            }
        }
    }
}
