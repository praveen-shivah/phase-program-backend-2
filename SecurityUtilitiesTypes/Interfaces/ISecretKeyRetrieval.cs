namespace SecurityUtilitiesTypes
{
    public interface ISecretKeyRetrieval
    {
        string GetKey();

        double GetRefreshTokenTTL();

        double GetJwtTokenTTL();
    }
}
