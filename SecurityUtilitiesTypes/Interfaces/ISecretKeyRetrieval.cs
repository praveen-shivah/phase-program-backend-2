namespace SecurityUtilitiesTypes
{
    public interface ISecretKeyRetrieval
    {
        string GetKey();

        double GetRefreshTokenTTLInDays();

        double GetJwtTokenTTLInMinutes();
    }
}
