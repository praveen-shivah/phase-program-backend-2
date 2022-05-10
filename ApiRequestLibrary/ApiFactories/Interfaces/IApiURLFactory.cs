namespace ApiRequestLibrary
{
    public enum ApiEndPointType
    {
        resellerBalance
    }

    public interface IApiURLFactory
    {
        string GetBaseURL();
        string GetURL(ApiEndPointType apiEndPointType);
    }
}
