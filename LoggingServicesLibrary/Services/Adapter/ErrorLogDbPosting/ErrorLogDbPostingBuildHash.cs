namespace LoggingServicesLibrary
{
    using DatabaseContext;

    using SharedUtilities;

    using System.Threading.Tasks;

    public class ErrorLogDbPostingBuildHash : IErrorLogDbPosting
    {
        private readonly IErrorLogDbPosting errorLogDbPosting;
        private readonly IGuidFactory guidFactory;

        public ErrorLogDbPostingBuildHash(IErrorLogDbPosting errorLogDbPosting, IGuidFactory guidFactory)
        {
            this.errorLogDbPosting = errorLogDbPosting;
            this.guidFactory = guidFactory;
        }

        async Task<ErrorLogDbPostingResponse> IErrorLogDbPosting.PostAsync(DataContext dataContext, ErrorLogDbPostingRequest errorLogDbPostingRequest)
        {
            var response = await this.errorLogDbPosting.PostAsync(dataContext, errorLogDbPostingRequest);
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