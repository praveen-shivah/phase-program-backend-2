namespace LoggingServicesLibrary
{
    using DataPostgresqlLibrary;

    public class SignificantEventLogDbPostingStart : ISignificantEventLogDbPosting
    {
        SignificantEventLogDbPostingResponse ISignificantEventLogDbPosting.Post(DPContext dataContext, SignificantEventLogDbPostingRequest significantEventLogDbPostingRequest)
        {
            return new SignificantEventLogDbPostingResponse { IsSuccessful = true };
        }
    }
}