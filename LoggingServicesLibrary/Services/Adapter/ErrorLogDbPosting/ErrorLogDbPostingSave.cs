namespace LoggingServicesLibrary
{
    using Database.Domain.Models.Library;
    using Database.Domain.Models.Library.Models;

    public class ErrorLogDbPostingSave : IErrorLogDbPosting
    {
        private readonly IErrorLogDbPosting errorLogDbPosting;

        public ErrorLogDbPostingSave(IErrorLogDbPosting errorLogDbPosting)
        {
            this.errorLogDbPosting = errorLogDbPosting;
        }

        ErrorLogDbPostingResponse IErrorLogDbPosting.Post(IDataContext dataContext, ErrorLogDbPostingRequest errorLogDbPostingRequest)
        {
            var response = this.errorLogDbPosting.Post(dataContext, errorLogDbPostingRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            var errorLog = new ErrorLog
                               {
                                   ClassName = errorLogDbPostingRequest.ClassName,
                                   LogClassId = (int)errorLogDbPostingRequest.LogClass,
                                   Message = errorLogDbPostingRequest.Message,
                                   MethodName = errorLogDbPostingRequest.MethodName,
                                   StackTrace = errorLogDbPostingRequest.StackTrace
                               };
            dataContext.ErrorLog.Add(errorLog);

            return response;
        }
    }
}