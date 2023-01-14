namespace RestServicesSupportTypes
{
    public enum RestServicesEnum
    {
        notSet,
        external,
    }

    public interface IRestServicesFactory<TRequest, TResponse> where TResponse : BaseResponseDto, new() where TRequest : class
    {
        IRestServices<TRequest, TResponse> Create(RestServicesEnum restServicesEnum);
    }
}
