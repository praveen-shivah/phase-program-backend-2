namespace DataSeedingLibrary
{
    using ApplicationLifeCycle;

    using CommonServices;

    using DatabaseContext;

    using LoggingLibrary;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    using SharedUtilities;

    using SimpleInjector;

    using System.Reflection;

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

        async Task<bool> IRequestLifeCycleStartupItem.ExecuteAsync()
        {
            try
            {
                var builder = new DbContextOptionsBuilder<DataContext>();
                var dbConnection = this.connectionFactory.Create();
                builder.UseNpgsql(dbConnection);
                var options = builder.Options;

                var context = new DataContext(options, this.dateTimeService);
                context.Database.SetCommandTimeout(15 * 600);
                await context.Database.MigrateAsync();
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
