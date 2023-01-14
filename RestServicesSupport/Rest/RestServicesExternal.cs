namespace RestServicesSupport
{
    using LoggingLibrary;

    using Newtonsoft.Json;

    using RestServicesSupportTypes;

    using RestSharp;

    public class RestServicesExternal<TRequest, TResponse> : IRestServices<TRequest, TResponse> where TResponse : BaseResponseDto, new() where TRequest : class
    {
        private readonly ILogger logger;

        public RestServicesExternal(ILogger logger)
        {
            this.logger = logger;
        }

        async Task<TResponse> IRestServices<TRequest, TResponse>.Post(string url, TRequest request)
        {
            var client = new RestClient(url);
            var body = JsonConvert.SerializeObject(request);
            TResponse? response;
            try
            {
                var restRequest = new RestRequest(url, Method.Post);
                restRequest.AddHeader("Content-Type", "application/json");
                this.logger.Info(LogClass.CommRest, $"RestServicesExternal.Post url {url} body {body}");
                restRequest.AddBody(body, "application/json");
                var restResponse = await client.ExecuteAsync(restRequest);

                this.logger.Info(LogClass.CommRest, $"RestServicesExternal.Post Response url {url} restResponse statusCode: {restResponse.StatusCode}");
                if (restResponse.ErrorMessage != null)
                {
                    this.logger.Info(LogClass.CommRest, $"RestServicesExternal.Post Response url {url} restResponse errorMessage: {restResponse.ErrorMessage}");
                }

                var output = restResponse.Content;
                if (output == null)
                {
                    this.logger.Info(LogClass.CommRest, $"RestServicesExternal.Post Response url {url} restResponse Content was null");
                    return new TResponse() { IsSuccessful = false };
                }

                response = JsonConvert.DeserializeObject<TResponse>(output);
                if (response == null)
                {
                    this.logger.Info(LogClass.CommRest, $"RestServicesExternal.Post Response url {url} restResponse.Content could not be deserialized");
                    return new TResponse() { IsSuccessful = false };
                }

                this.logger.Info(LogClass.CommRest, $"RestServicesExternal.Post url {url} body {body} response: {response}");
                response.IsSuccessful = true;
                return response;
            }
            catch (Exception ex)
            {
                this.logger.Info(LogClass.CommRest, $"ERROR: RestServicesExternal.Post url {url} body {body} {ex.Message} {ex.StackTrace}");
                return new TResponse() { IsSuccessful = false };
            }
        }
    }
}