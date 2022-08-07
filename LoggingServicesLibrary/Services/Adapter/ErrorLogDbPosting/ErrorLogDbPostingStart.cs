namespace LoggingServicesLibrary
{
    using System.Threading.Tasks;

    using DataPostgresqlLibrary;

    public class ErrorLogDbPostingStart : IErrorLogDbPosting
    {
        Task<ErrorLogDbPostingResponse> IErrorLogDbPosting.PostAsync(DPContext dataContext, ErrorLogDbPostingRequest errorLogDbPostingRequest)
        {
            return Task.FromResult(new ErrorLogDbPostingResponse() { IsSuccessful = true });
        }
    }
}
