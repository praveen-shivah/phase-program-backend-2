namespace LoggingServicesLibrary
{
    using System.Threading.Tasks;

    using DataPostgresqlLibrary;

    public interface IErrorLogDbPosting
    {
        Task<ErrorLogDbPostingResponse> PostAsync(DPContext dataContext, ErrorLogDbPostingRequest errorLogDbPostingRequest);
    }
}
