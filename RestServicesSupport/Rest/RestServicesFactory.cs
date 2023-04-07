namespace RestServicesSupport
{
    using APISupportTypes;

    using LoggingLibrary;

    public class RestServicesFactory<TRequest, TResponse> : IRestServicesFactory<TRequest, TResponse> where TResponse : BaseResponseDto, new() where TRequest : class
    {
        private readonly ILogger logger;

        public RestServicesFactory(ILogger logger)
        {
            this.logger = logger;
        }

        IRestServices<TRequest, TResponse> IRestServicesFactory<TRequest, TResponse>.Create(RestServicesEnum restServicesEnum)
        {
            switch (restServicesEnum)
            {
                case RestServicesEnum.external:
                    return new RestServicesExternal<TRequest, TResponse>(this.logger);
                default:
                    throw new ArgumentOutOfRangeException(nameof(restServicesEnum), restServicesEnum, null);
            }
        }
    }
}
