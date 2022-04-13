namespace LoggingServicesLibrary
{
    using System.Linq;

    using CommonServices;

    using Database.Domain.Models.Library;

    public class ErrorLogDbPostingCheckForExcessive : IErrorLogDbPosting
    {
        private readonly IErrorLogDbPosting errorLogDbPosting;
        private readonly IDateTimeService dateTimeService;

        public ErrorLogDbPostingCheckForExcessive(IErrorLogDbPosting errorLogDbPosting, IDateTimeService dateTimeService)
        {
            this.errorLogDbPosting = errorLogDbPosting;
            this.dateTimeService = dateTimeService;
        }

        ErrorLogDbPostingResponse IErrorLogDbPosting.Post(IDataContext dataContext, ErrorLogDbPostingRequest errorLogDbPostingRequest)
        {
            var response = this.errorLogDbPosting.Post(dataContext, errorLogDbPostingRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            // Only look at the last 24 hours
            var dateToLookAt = this.dateTimeService.UtcNow.AddDays(-1);
            var currentCount = dataContext.ErrorLog.Count(x => x.Hash == response.Hash && x.CreatedOn >= dateToLookAt);
            if (currentCount > 100)
            {
                response.IsSuccessful = false;
            }

            return response;
        }
    }
}