namespace ApiRequestLibrary
{
    public enum ApiEndPointType
    {
        resellerBalance
    }

    public interface IApiURLFactory
    {
        string GetURL(ApiEndPointType apiEndPointType);
    }
}
