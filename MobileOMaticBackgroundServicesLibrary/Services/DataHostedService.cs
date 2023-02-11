namespace MobileOMaticBackgroundServicesLibrary
{
    using LoggingLibrary;

    using Microsoft.Extensions.Hosting;

    public class DataHostedService : BackgroundService
    {
        private readonly ILogger logger;

        public DataHostedService(ILogger logger)
        {
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            this.logger.Debug(LogClass.General, "Consume Scoped Service Hosted Service running.");

            await this.DoWork(stoppingToken);
        }

        private Task DoWork(CancellationToken stoppingToken)
        {
            // This must be async so that it returns

            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    try
            //    {
            //    }
            //    catch (Exception ex)
            //    {
            //        this.logger.Error(LogClass.General, "DataHostedService", "DoWork", $"Error: {ex.Message} {ex.StackTrace}", ex);
            //    }
            //}

            return Task.CompletedTask;
        }
    }
}
