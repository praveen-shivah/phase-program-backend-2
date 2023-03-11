namespace AuthenticationRepository
{
    using AuthenticationRepositoryTypes;

    using SecurityUtilitiesTypes;

    public interface IJwtValidate
    {
        JwtValidateResponse ValidateJwtToken(string token, ISecretKeyRetrieval secretKeyRetrieval);
    }
}
