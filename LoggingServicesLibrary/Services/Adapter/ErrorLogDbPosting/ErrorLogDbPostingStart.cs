namespace LoggingServicesLibrary
{
    using Database.Domain.Models.Library;

    public class ErrorLogDbPostingStart : IErrorLogDbPosting
    {
        ErrorLogDbPostingResponse IErrorLogDbPosting.Post(IDataContext dataContext, ErrorLogDbPostingRequest errorLogDbPostingRequest)
        {
            return new ErrorLogDbPostingResponse() { IsSuccessful = true };
        }
    }
}
