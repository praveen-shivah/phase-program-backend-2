namespace DataSeedingLibrary
{
    using System.Reflection;

    using ApplicationLifeCycle;

    using CommonServices;

    using DataPostgresqlLibrary;

    using LoggingLibrary;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    using SharedUtilities;

    using SimpleInjector;

    public class RequestLifeCycleStartupItemMigrations : IRequestLifeCycleStartupItem
    {
        private readonly ILogger logger;
        private readonly Container container;

        private readonly IConfiguration configuration;

        private readonly IConnectionFactory connectionFactory;

        private readonly IDateTimeService dateTimeService;

        public RequestLifeCycleStartupItemMigrations(ILogger logger, Container container, IConfiguration configuration, IConnectionFactory connectionFactory, IDateTimeService dateTimeService)
        {
            this.logger = logger;
            this.container = container;
            this.configuration = configuration;
            this.connectionFactory = connectionFactory;
            this.dateTimeService = dateTimeService;
        }

        RequestLifeCycleStartupItemPriority IRequestLifeCycleStartupItem.RequestLifeCycleStartupItemPriority => RequestLifeCycleStartupItemPriority.migration;

        bool IRequestLifeCycleStartupItem.Execute()
        {
            try
            {
                var builder = new DbContextOptionsBuilder<DPContext>();
                var dbConnection = this.connectionFactory.Create();
                builder.UseNpgsql(dbConnection);
                var options = builder.Options;

                var context = new DPContext(options, this.configuration, this.dateTimeService);
                context.Database.SetCommandTimeout(15 * 600);
                context.Database.MigrateAsync().Wait();
            }
            catch (Exception e)
            {
                this.logger.Error(LogClass.General, this.GetType().Name, MethodBase.GetCurrentMethod()?.Name, "Error setting performing database migrations.", e);
                return false;
            }

            return true;
        }
    }

}
