namespace RestServicesSupportTypes
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRestServices<TRequest, TResponse>
        where TResponse : BaseResponseDto, new() where TRequest : class
    {
        Task<TResponse> Post(
            string url,
            TRequest request,
            Dictionary<string, string> headerDictionary);

        Task<TResponse> Post(string url, TRequest request);

        Task<TResponse> Post(string url, string jwtTokenString, TRequest request);
    }
}