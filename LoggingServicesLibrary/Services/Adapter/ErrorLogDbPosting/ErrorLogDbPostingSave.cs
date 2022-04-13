namespace LoggingServicesLibrary
{
    using DataModelsLibrary;

    using DataPostgresqlLibrary;

    public class ErrorLogDbPostingSave : IErrorLogDbPosting
    {
        private readonly IErrorLogDbPosting errorLogDbPosting;

        public ErrorLogDbPostingSave(IErrorLogDbPosting errorLogDbPosting)
        {
            this.errorLogDbPosting = errorLogDbPosting;
        }

        ErrorLogDbPostingResponse IErrorLogDbPosting.Post(DPContext dataContext, ErrorLogDbPostingRequest errorLogDbPostingRequest)
        {
            var response = this.errorLogDbPosting.Post(dataContext, errorLogDbPostingRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            var errorLog = new ErrorLog
            {
                ClassName = errorLogDbPostingRequest.ClassName ?? string.Empty,
                LogClassId = (int)errorLogDbPostingRequest.LogClass,
                Message = errorLogDbPostingRequest.Message ?? string.Empty,
                MethodName = errorLogDbPostingRequest.MethodName ?? string.Empty,
                StackTrace = errorLogDbPostingRequest.StackTrace ?? string.Empty,
                Hash = response.Hash ?? string.Empty
            };

            dataContext.ErrorLog.Add(errorLog);

            return response;
        }
    }
}