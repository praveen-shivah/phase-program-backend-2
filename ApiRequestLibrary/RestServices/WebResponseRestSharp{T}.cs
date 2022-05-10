namespace ApiRequestLibrary
{
    using RestSharp;

    public class WebResponseRestSharp<T> : WebResponseRestSharp, IWebResponse<T>
        where T : class
    {
        private readonly RestResponse<T> response;

        public WebResponseRestSharp(RestResponse<T> response)
            : base(response)
        {
            this.response = response ?? throw new ArgumentNullException(nameof(response));
        }

        public T Data => this.response.Data;
    }
}
