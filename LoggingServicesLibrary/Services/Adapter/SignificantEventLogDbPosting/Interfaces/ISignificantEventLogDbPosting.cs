namespace LoggingServicesLibrary
{
    using DatabaseContext;

    public interface ISignificantEventLogDbPosting
    {
        SignificantEventLogDbPostingResponse Post(DataContext dataContext, SignificantEventLogDbPostingRequest significantEventLogDbPostingRequest);
    }
}
