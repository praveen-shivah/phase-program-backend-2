namespace AutomaticTaskQueueLibrary;

using DataPostgresqlLibrary;

using UnitOfWorkTypesLibrary;

public class AutomaticTaskQueueServiceProcessorRepository : IAutomaticTaskQueueServiceProcessorRepository
{
    private readonly IUnitOfWorkFactory<DPContext> unitOfWorkFactory;

    private readonly IAutomaticTaskQueueServiceProcessor automaticTaskQueueServiceProcessor;

    public AutomaticTaskQueueServiceProcessorRepository(IUnitOfWorkFactory<DPContext> unitOfWorkFactory, IAutomaticTaskQueueServiceProcessor automaticTaskQueueServiceProcessor)
    {
        this.unitOfWorkFactory = unitOfWorkFactory;
        this.automaticTaskQueueServiceProcessor = automaticTaskQueueServiceProcessor;
    }

    async Task<AutomaticTaskQueueServiceProcessorResponse> IAutomaticTaskQueueServiceProcessorRepository.AutomaticTaskQueueServiceProcessorAsync(AutomaticTaskQueueServiceProcessorRequest request)
    {
        var result = new AutomaticTaskQueueServiceProcessorResponse();
        var uow = this.unitOfWorkFactory.Create(
            async context =>
                {
                    var response = await this.automaticTaskQueueServiceProcessor.AutomaticTaskQueueServiceProcessorAsync(context, request);
                    result.IsSuccessful = response.IsSuccessful;

                    if (response.IsSuccessful)
                    {
                        return WorkItemResultEnum.commitSuccessfullyCompleted;
                    }
                    else
                    {
                        return WorkItemResultEnum.rollbackExit;
                    }
                });
        var uowResult = await uow.ExecuteAsync();
        if (uowResult != WorkItemResultEnum.commitSuccessfullyCompleted)
        {
            result.IsSuccessful = false;
        }

        return result;
    }
}