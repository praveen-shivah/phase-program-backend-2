namespace LoggingServicesLibrary
{
    using System;

    using DataModelsLibrary;

    using DataPostgresqlLibrary;

    using log4net;

    using LoggingLibrary;

    using UnitOfWorkTypesLibrary;

    using SignificantEventType = LoggingLibraryTypes.SignificantEventType;

    /// <summary>
    ///     LoggerAdapter - used to start moving towards log4net.
    ///     The calls are the same now but they are not used to differentiate them.
    /// </summary>
    public class LoggerAdapterDb : LoggerAdapter,
                                   ILoggerAdapter
    {
        private readonly IErrorLogDbPosting errorLogDbPosting;
        private readonly ILog log4Net;
        private readonly IEntityContextFrameWorkFactory<DPContext> entityContextFrameWorkFactory;
        private readonly ISignificantEventLogDbPosting significantEventLogDbPosting;

        public LoggerAdapterDb(ILog log4Net,
                               IEntityContextFrameWorkFactory<DPContext> entityContextFrameWorkFactory,
                               IErrorLogDbPosting errorLogDbPosting,
                               ISignificantEventLogDbPosting significantEventLogDbPosting)
            : base(log4Net)
        {
            this.log4Net = log4Net;
            this.entityContextFrameWorkFactory = entityContextFrameWorkFactory;
            this.errorLogDbPosting = errorLogDbPosting;
            this.significantEventLogDbPosting = significantEventLogDbPosting;
        }

        void ILogger.Error(LogClass logClass,
                           string className,
                           string method,
                           string message,
                           Exception exception)
        {
            this.log4Net.Error(message, exception);
            try
            {
                using (var context = this.entityContextFrameWorkFactory.CreateContext())
                {
                    var errorLogDbPostingRequest = new ErrorLogDbPostingRequest(
                        className,
                        logClass,
                        message,
                        method,
                        exception == null ? Environment.StackTrace : exception.StackTrace);
                    var task = this.errorLogDbPosting.PostAsync(context, errorLogDbPostingRequest);
                    task.Wait();
                    if (task.Result.IsSuccessful)
                    {
                        var significantEventLogDbPostingRequest = new SignificantEventLogDbPostingRequest(
                            0,
                            SignificantEventType.GeneralError,
                            "General Error",
                            message);
                        var significantEventLogDbPostingResponse = this.significantEventLogDbPosting.Post(context, significantEventLogDbPostingRequest);

                        if (significantEventLogDbPostingResponse.IsSuccessful)
                        {
                            context.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                try
                {
                    Console.WriteLine($"Error writing out to ErrorLog table: {logClass},{className},{method},{message},{exception?.StackTrace} ");
                    Console.WriteLine($"New error: {e}");
                }
                catch
                {
                }
            }
        }

        void ILogger.Event(SignificantEventType significantEventType,
                           string shortDescription,
                           string longDescription)
        {
            var significantEvent = new SignificantEvent
            {
                CreatedBy = 0,
                EventTypeId = (int)significantEventType,
                ShortDescription = shortDescription,
                LongDescription = longDescription
            };
            this.logSignificantEvent(significantEvent);
        }

        void ILogger.Event(SignificantEventType significantEventType,
                           string shortDescription,
                           string longDescription,
                           DateTime createdOn)
        {
            var significantEvent = new SignificantEvent
            {
                CreatedBy = 0,
                CreatedOn = createdOn,
                EventTypeId = (int)significantEventType,
                ShortDescription = shortDescription,
                LongDescription = longDescription
            };
            this.logSignificantEvent(significantEvent);
        }

        private void logSignificantEvent(SignificantEvent significantEvent)
        {
            using (var context = this.entityContextFrameWorkFactory.CreateContext())
            {
                context.SignificantEvent.Add(significantEvent);
                context.SaveChanges();
            }
        }
    }
}