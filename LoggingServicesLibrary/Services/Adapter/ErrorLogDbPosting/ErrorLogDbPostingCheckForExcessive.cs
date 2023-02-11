namespace LoggingServicesLibrary
{
    using CommonServices;

    using DatabaseContext;

    using Microsoft.EntityFrameworkCore;

    using System.Threading.Tasks;

    public class ErrorLogDbPostingCheckForExcessive : IErrorLogDbPosting
    {
        private readonly IErrorLogDbPosting errorLogDbPosting;
        private readonly IDateTimeService dateTimeService;

        public ErrorLogDbPostingCheckForExcessive(IErrorLogDbPosting errorLogDbPosting, IDateTimeService dateTimeService)
        {
            this.errorLogDbPosting = errorLogDbPosting;
            this.dateTimeService = dateTimeService;
        }

        async Task<ErrorLogDbPostingResponse> IErrorLogDbPosting.PostAsync(DataContext dataContext, ErrorLogDbPostingRequest errorLogDbPostingRequest)
        {
            var response = await this.errorLogDbPosting.PostAsync(dataContext, errorLogDbPostingRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            // Only look at the last 24 hours
            var dateToLookAt = this.dateTimeService.UtcNow.AddDays(-1);
            var currentCount = await dataContext.ErrorLog.CountAsync(x => x.Hash == response.Hash && x.CreatedOn >= dateToLookAt);
            if (currentCount > 100)
            {
                response.IsSuccessful = false;
            }

            return response;
        }
    }
}