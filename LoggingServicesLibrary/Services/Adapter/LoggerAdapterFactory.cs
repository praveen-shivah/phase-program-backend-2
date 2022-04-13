namespace LoggingServicesLibrary
{
    using DataPostgresqlLibrary;

    using log4net;

    using LoggingLibrary;

    using UnitOfWorkTypesLibrary;

    public class LoggerAdapterFactory : ILoggerAdapterFactory
    {
        private readonly IEntityContextFrameWorkFactory<DPContext> entityContextFrameWorkFactory;
        private readonly IErrorLogDbPosting errorLogDbPosting;
        private readonly ISignificantEventLogDbPosting significantEventLogDbPosting;

        public LoggerAdapterFactory(IEntityContextFrameWorkFactory<DPContext> entityContextFrameWorkFactory, IErrorLogDbPosting errorLogDbPosting, ISignificantEventLogDbPosting significantEventLogDbPosting)
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
