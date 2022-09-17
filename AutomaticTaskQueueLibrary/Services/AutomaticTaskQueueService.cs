namespace AutomaticTaskQueueLibrary;

using LoggingLibrary;

using Microsoft.Extensions.Hosting;

public class AutomaticTaskQueueService : BackgroundService
{
    private readonly ILogger logger;

    private readonly IAutomaticTaskQueueServiceProcessorRepository automaticTaskQueueServiceProcessorRepository;

    public AutomaticTaskQueueService(ILogger logger, IAutomaticTaskQueueServiceProcessorRepository automaticTaskQueueServiceProcessorRepository)
    {
        this.logger = logger;
        this.automaticTaskQueueServiceProcessorRepository = automaticTaskQueueServiceProcessorRepository;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.Debug(LogClass.General, "AutomaticTaskQueueService is starting.");

        stoppingToken.Register(() => logger.Debug(LogClass.General, " GracePeriod background task is stopping."));

        while (!stoppingToken.IsCancellationRequested)
        {
            var response = await this.automaticTaskQueueServiceProcessorRepository.AutomaticTaskQueueServiceProcessorAsync(new AutomaticTaskQueueServiceProcessorRequest());

            await Task.Delay(20000, stoppingToken);
        }

        this.logger.Debug(LogClass.General, "GracePeriod background task is stopping.");
    }
}