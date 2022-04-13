namespace LoggingServicesLibrary
{
    using DataPostgresqlLibrary;

    using SharedUtilities;

    public class ErrorLogDbPostingBuildHash : IErrorLogDbPosting
    {
        private readonly IErrorLogDbPosting errorLogDbPosting;
        private readonly IGuidFactory guidFactory;

        public ErrorLogDbPostingBuildHash(IErrorLogDbPosting errorLogDbPosting, IGuidFactory guidFactory)
        {
            this.errorLogDbPosting = errorLogDbPosting;
            this.guidFactory = guidFactory;
        }

        ErrorLogDbPostingResponse IErrorLogDbPosting.Post(DPContext dataContext, ErrorLogDbPostingRequest errorLogDbPostingRequest)
        {
            var response = this.errorLogDbPosting.Post(dataContext, errorLogDbPostingRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            var guid = this.guidFactory.Create(
                $"{errorLogDbPostingRequest.ClassName}{errorLogDbPostingRequest.MethodName}{errorLogDbPostingRequest.LogClass.ToString()}{errorLogDbPostingRequest.Message}{errorLogDbPostingRequest.StackTrace}");

            response.Hash = guid.ToString();

            return response;
        }
    }
}