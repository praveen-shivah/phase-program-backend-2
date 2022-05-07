namespace ApiRequestLibrary
{
    public class ApiURLFactory : IApiURLFactory
    {
        private string apiUrlBase = "localhost";

        string IApiURLFactory.GetURL(ApiEndPointType apiEndPointType)
        {
            switch (apiEndPointType)
            {
                case ApiEndPointType.resellerBalance:
                    return $"{this.apiUrlBase}/api/reseller/reseller-balance";
                default:
                    throw new ArgumentOutOfRangeException(nameof(apiEndPointType), apiEndPointType, null);
            }
        }
    }
}
