namespace RestServicesSupportTypes
{
    public interface IRestServices<TRequest, TResponse> where TResponse : BaseResponseDto, new() where TRequest : class
    {
        Task<TResponse> Post(string url, TRequest request);
    }
}
