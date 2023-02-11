namespace LoggingServicesLibrary
{
    using DatabaseContext;

    using System.Threading.Tasks;

    public class ErrorLogDbPostingStart : IErrorLogDbPosting
    {
        Task<ErrorLogDbPostingResponse> IErrorLogDbPosting.PostAsync(DataContext dataContext, ErrorLogDbPostingRequest errorLogDbPostingRequest)
        {
            return Task.FromResult(new ErrorLogDbPostingResponse() { IsSuccessful = true });
        }
    }
}
