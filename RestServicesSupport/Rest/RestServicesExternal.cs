namespace RestServicesSupport
{
    using LoggingLibrary;

    using Newtonsoft.Json;

    using RestSharp;

    using System.Net;

    using APISupportTypes;

    public class RestServicesExternal<TRequest, TResponse> : IRestServices<TRequest, TResponse> where TResponse : BaseResponseDto, new() where TRequest : class
    {
        private readonly ILogger logger;

        public RestServicesExternal(ILogger logger)
        {
            this.logger = logger;
        }

        async Task<TResponse> IRestServices<TRequest, TResponse>.Post(string url, TRequest request)
        {
            return await ((IRestServices<TRequest, TResponse>)this).Post(url, request, new Dictionary<string, string>());
        }

        async Task<TResponse> IRestServices<TRequest, TResponse>.Post(
            string url,
            string jwtTokenString,
            TRequest request)
        {
            var headerDictionary = new Dictionary<string, string>();
            headerDictionary.Add("Access-Token", jwtTokenString);

            return await ((IRestServices<TRequest, TResponse>)this).Post(url, request, headerDictionary);
        }

        async Task<TResponse> IRestServices<TRequest, TResponse>.Post(string url, TRequest request, Dictionary<string, string> headerDictionary)
        {
            var client = new RestClient(url);
            var body = JsonConvert.SerializeObject(request);
            TResponse? response;
            try
            {
                var restRequest = new RestRequest(url, Method.Post);
                restRequest.AddHeader("Content-Type", "application/json");
                this.logger.Info(LogClass.CommRest, $"RestServicesExternal.Post url {url} body {body}");
                foreach (var header in headerDictionary)
                {
                    restRequest.AddHeader(header.Key, header.Value);
                }

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
                    return new TResponse()
                    {
                        IsSuccessful = false,
                        HttpStatusCode = restResponse.StatusCode,
                        ResponseTypeEnum = ResponseTypeEnum.cannotReach3rdParty,
                        ErrorMessage = restResponse.ErrorMessage ?? string.Empty
                    };
                }

                response = JsonConvert.DeserializeObject<TResponse>(output);
                if (response == null)
                {
                    this.logger.Info(LogClass.CommRest, $"RestServicesExternal.Post Response url {url} restResponse.Content could not be deserialized");
                    return new TResponse()
                    {
                        IsSuccessful = false,
                        HttpStatusCode = restResponse.StatusCode,
                        ResponseTypeEnum = ResponseTypeEnum.invalidObjectReturned,
                        ErrorMessage = $"Cannot deserialize response {output}"
                    };
                }

                this.logger.Info(LogClass.CommRest, $"RestServicesExternal.Post url {url} body {body} response: {response}");
                response.IsSuccessful = true;
                response.ResponseTypeEnum = ResponseTypeEnum.success;
                response.HttpStatusCode = restResponse.StatusCode;
                response.ErrorMessage = restResponse.ErrorMessage ?? string.Empty;

                return response;
            }
            catch (Exception ex)
            {
                this.logger.Info(LogClass.CommRest, $"ERROR: RestServicesExternal.Post url {url} body {body} {ex.Message} {ex.StackTrace}");
                return new TResponse()
                {
                    IsSuccessful = false,
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    ErrorMessage = $"Error {ex.Message}",
                    ResponseTypeEnum = ResponseTypeEnum.cannotReach3rdParty
                };
            }
        }
    }
}