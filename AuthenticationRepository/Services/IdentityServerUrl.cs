namespace AuthenticationRepository
{
    using LoggingLibrary;

    using Microsoft.Extensions.Configuration;

    using System;

    public class IdentityServerUrl : IIdentityServerUrl
    {
        private readonly ILogger logger;

        private readonly IConfiguration configuration;

        public IdentityServerUrl(ILogger logger, IConfiguration configuration)
        {
            this.logger = logger;
            this.configuration = configuration;
        }

        string IIdentityServerUrl.GetURL()
        {
            try
            {
                return this.configuration.GetSection("IdentityServer:Url").Value;
            }
            catch (Exception e)
            {
                this.logger.Error(LogClass.Configuration, "IdentityServerUrl", "GetUrl", $"Error {e.Message} {e.StackTrace}", e);
            }

            return "http://localhost/";
        }
    }
}