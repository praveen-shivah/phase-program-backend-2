namespace LoggingServicesLibrary
{
    using DatabaseContext;

    public class SignificantEventLogDbPostingStart : ISignificantEventLogDbPosting
    {
        SignificantEventLogDbPostingResponse ISignificantEventLogDbPosting.Post(DataContext dataContext, SignificantEventLogDbPostingRequest significantEventLogDbPostingRequest)
        {
            return new SignificantEventLogDbPostingResponse { IsSuccessful = true };
        }
    }
}