namespace ApiRequestLibrary
{
    using System;
    using System.Collections.Generic;

    using RestSharp;

    public class WebResponseRestSharp : IWebResponse
    {
        private readonly RestResponse response;

        public WebResponseRestSharp(RestResponse response)
        {
            this.response = response ?? throw new ArgumentNullException(nameof(response));
        }

        public string Error => this.response.ErrorMessage;

        public string Text => this.response.Content;

        public IReadOnlyList<byte> Bytes => this.response.RawBytes;

        public bool IsSuccessful => this.response.IsSuccessful;
    }
}