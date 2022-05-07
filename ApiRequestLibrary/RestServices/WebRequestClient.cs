namespace ApiRequestLibrary
{
    using RestSharp;

    public class WebRequestClient : IWebRequestClient
    {
        private readonly RestClient restClient;

        public WebRequestClient()
        {
            this.restClient = new RestClient();
        }

        async Task<IWebResponse<T>> IWebRequestClient.GetAsync<T>(string uri, CancellationToken cancellationToken)
        {
            var request = new RestRequest(uri, Method.Get);
            var response = await this.restClient.ExecuteAsync<T>(request, cancellationToken);
            return new WebResponseRestSharp<T>(response);
        }

        async Task<IWebResponse<T>> IWebRequestClient.PostAsync<T>(
            string uri,
            object postData,
            CancellationToken cancellationToken)
        {
            var request = new RestRequest(uri, Method.Post);
            var response = await this.restClient.ExecuteAsync<T>(request, cancellationToken);
            return new WebResponseRestSharp<T>(response);
        }
    }
}
