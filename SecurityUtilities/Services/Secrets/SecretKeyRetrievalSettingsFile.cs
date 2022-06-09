namespace SecurityUtilities
{
    using Microsoft.Extensions.Configuration;

    using SecurityUtilitiesTypes;

    public class SecretKeyRetrievalSettingsFile : ISecretKeyRetrieval
    {
        private readonly IConfiguration configuration;

        public SecretKeyRetrievalSettingsFile(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        string ISecretKeyRetrieval.GetKey()
        {
            return this.configuration.GetSection("AppSettings:Token").Value;
        }

        // number of days
        double ISecretKeyRetrieval.GetRefreshTokenTTL()
        {
            if (!double.TryParse(this.configuration.GetSection("AppSettings:RefreshTokenTTL").Value, out double result))
            {
                result = 7.0;
            }

            return result;
        }

        // Number of minutes
        double ISecretKeyRetrieval.GetJwtTokenTTL()
        {
            if (!double.TryParse(this.configuration.GetSection("AppSettings:JwtTokenTTL").Value, out double result))
            {
                result = 1.0;
            }

            return result;
        }
    }
}
