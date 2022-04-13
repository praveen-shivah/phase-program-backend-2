namespace LoggingServicesLibrary
{
    using log4net;

    using LoggingLibrary;

    public class LoggerAdapterFactory : ILoggerAdapterFactory
    {
        private readonly IEntityContextFrameWorkFactory entityContextFrameWorkFactory;
        private readonly IErrorLogDbPosting errorLogDbPosting;
        private readonly ISignificantEventLogDbPosting significantEventLogDbPosting;

        public LoggerAdapterFactory(IEntityContextFrameWorkFactory entityContextFrameWorkFactory, IErrorLogDbPosting errorLogDbPosting, ISignificantEventLogDbPosting significantEventLogDbPosting)
        {
            this.entityContextFrameWorkFactory = entityContextFrameWorkFactory;
            this.errorLogDbPosting = errorLogDbPosting;
            this.significantEventLogDbPosting = significantEventLogDbPosting;
        }

        ILoggerAdapter ILoggerAdapterFactory.Create(object log4Net)
        {
            return new LoggerAdapterDb((ILog)log4Net, this.entityContextFrameWorkFactory, this.errorLogDbPosting, this.significantEventLogDbPosting);
        }
    }
}
