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
                case ApiEndPointType.resellerBalance:
                    return $"api/reseller/reseller-balance";
                default:
                    throw new ArgumentOutOfRangeException(nameof(apiEndPointType), apiEndPointType, null);
            }
        }
    }
}
