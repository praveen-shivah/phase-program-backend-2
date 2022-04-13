namespace LoggingServicesLibrary
{
    using DataPostgresqlLibrary;

    public class ErrorLogDbPostingStart : IErrorLogDbPosting
    {
        ErrorLogDbPostingResponse IErrorLogDbPosting.Post(DPContext dataContext, ErrorLogDbPostingRequest errorLogDbPostingRequest)
        {
            return new ErrorLogDbPostingResponse() { IsSuccessful = true };
        }
    }
}
