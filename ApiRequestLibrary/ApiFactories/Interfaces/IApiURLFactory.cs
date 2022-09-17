namespace ApiRequestLibrary
{
    public enum ApiEndPointType
    {
        resellerBalance,
        resellerTransferPointsCompleted
    }

    public interface IApiURLFactory
    {
        string GetBaseURL();
        string GetURL(ApiEndPointType apiEndPointType);
    }
}
