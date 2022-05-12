namespace ApiRequestLibrary
{
    using ApiDTO;

    using RestSharp;

    public class WebRequestClient : IWebRequestClient
    {
        async Task<IWebResponse<T>> IWebRequestClient.GetAsync<T>(string baseUrl, string uri, CancellationToken cancellationToken)
        {
            var restClient = new RestClient(baseUrl);
            var request = new RestRequest(uri, Method.Get);
            var response = await restClient.ExecuteAsync<T>(request, cancellationToken);
            return new WebResponseRestSharp<T>(response);
        }

        async Task<IWebResponse<T>> IWebRequestClient.PostAsync<T>(
            string baseUrl,
            string uri,
            CallBackInformationDTO postData,
            CancellationToken cancellationToken)
        {
            var restClient = new RestClient(baseUrl);
            var request = new RestRequest(uri, Method.Post);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("OrganizationId", postData.OrganizationId);
            request.AddHeader("APIKey", postData.APIKey);
            request.AddJsonBody(postData);

            var response = await restClient.ExecuteAsync<T>(request, cancellationToken);
            return new WebResponseRestSharp<T>(response);
        }
    }
}
