namespace ApiRequestLibrary
{
    public interface IWebResponse<out T> : IWebResponse where T : class
    {
        T Data { get; }
    }
}
