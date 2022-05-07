namespace ApiRequestLibrary
{
    public interface IWebRequestClient
    {
        Task<IWebResponse<T>> GetAsync<T>(string uri, CancellationToken cancellationToken = default)
            where T : class;

        Task<IWebResponse<T>> PostAsync<T>(string uri, object postData, CancellationToken cancellationToken = default)
            where T : class;
    }
}
