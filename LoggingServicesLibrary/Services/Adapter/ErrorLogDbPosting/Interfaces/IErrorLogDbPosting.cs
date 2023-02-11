namespace LoggingServicesLibrary
{
    using DatabaseContext;

    using System.Threading.Tasks;

    public interface IErrorLogDbPosting
    {
        Task<ErrorLogDbPostingResponse> PostAsync(DataContext dataContext, ErrorLogDbPostingRequest errorLogDbPostingRequest);
    }
}
