namespace LoggingServicesLibrary
{
    using Database.Domain.Models.Library;

    public interface IErrorLogDbPosting
    {
        ErrorLogDbPostingResponse Post(IDataContext dataContext, ErrorLogDbPostingRequest errorLogDbPostingRequest);
    }
}
