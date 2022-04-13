namespace LoggingServicesLibrary
{
    using DataPostgresqlLibrary;

    public interface ISignificantEventLogDbPosting
    {
        SignificantEventLogDbPostingResponse Post(DPContext dataContext, SignificantEventLogDbPostingRequest significantEventLogDbPostingRequest);
    }
}
