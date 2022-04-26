namespace ZohoBooksService
{
    using System.Text;

    using RestSharp;
    using RestSharp.Authenticators.OAuth2;

    public class ZohoBooksService
    {
        private RestClient restClient;

        public ZohoBooksService(string apiKey, string apiKeySecret)
        {
            var clientId = "1000.H8Y9SZNAS8FVAX3901J9R3H98RXE2E";
            var clientSecret = "13ec650e885997d37f7e916c48feb912ac65fe455a";
            var scope = "Zohobooks.All";
            var responseType = "code";
            var redirectUrl = string.Empty;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"scope={scope}");
            sb.AppendLine($"response_type={responseType}");
            sb.AppendLine($"redirect_url={redirectUrl}");
            sb.AppendLine($"client_id={clientId}");

            var options = new RestClientOptions($"https://accounts.zoho.com/oauth/v2/auth?{sb.ToString()}");
            // this.restClient = new RestClient(options) { Authenticator = new OAuth2UriQueryParameterAuthenticator("test") };
            this.restClient = new RestClient("https://accounts.zoho.com/oauth/v2/auth");

        }
    }
}
