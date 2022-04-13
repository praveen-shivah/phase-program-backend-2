namespace LoggingServicesLibrary
{
    using DataPostgresqlLibrary;

    public interface IErrorLogDbPosting
    {
        ErrorLogDbPostingResponse Post(DPContext dataContext, ErrorLogDbPostingRequest errorLogDbPostingRequest);
    }
}
