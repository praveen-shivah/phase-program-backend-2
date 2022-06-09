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
        double ISecretKeyRetrieval.GetRefreshTokenTTLInDays()
        {
            if (!double.TryParse(this.configuration.GetSection("AppSettings:RefreshTokenTTLInDays").Value, out double result))
            {
                result = 7.0;
            }

            return result;
        }

        // Number of minutes
        double ISecretKeyRetrieval.GetJwtTokenTTLInMinutes()
        {
            if (!double.TryParse(this.configuration.GetSection("AppSettings:JwtTokenTTLInMinutes").Value, out double result))
            {
                result = 1.0;
            }

            return result;
        }
    }
}
