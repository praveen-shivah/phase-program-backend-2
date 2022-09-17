namespace AutomaticTaskQueueLibrary;

using ApiDTO;

using AutomaticTaskSharedLibrary;

using CommonServices;

using DataPostgresqlLibrary;

using InvoiceRepository;

public class AutomaticTaskQueueServiceProcessorProcess : IAutomaticTaskQueueServiceProcessor
{
    private IAutomaticTaskQueueServiceProcessor automaticTaskQueueServiceProcessor;

    private readonly IDistributorToOperatorSendPointsTransfer distributorToOperatorSendPointsTransfer;

    private readonly IDateTimeService dateTimeService;

    public AutomaticTaskQueueServiceProcessorProcess(IAutomaticTaskQueueServiceProcessor automaticTaskQueueServiceProcessor,
                                                     IDistributorToOperatorSendPointsTransfer distributorToOperatorSendPointsTransfer,
                                                     IDateTimeService dateTimeService)
    {
        this.automaticTaskQueueServiceProcessor = automaticTaskQueueServiceProcessor;
        this.distributorToOperatorSendPointsTransfer = distributorToOperatorSendPointsTransfer;
        this.dateTimeService = dateTimeService;
    }

    async Task<AutomaticTaskQueueServiceProcessorResponse> IAutomaticTaskQueueServiceProcessor.AutomaticTaskQueueServiceProcessorAsync(
        DPContext context,
        AutomaticTaskQueueServiceProcessorRequest request)
    {
        var response = await this.automaticTaskQueueServiceProcessor.AutomaticTaskQueueServiceProcessorAsync(context, request);
        if (!response.IsSuccessful || response.QueueRecord == null)
        {
            return response;
        }


        await this.distributorToOperatorSendPointsTransfer.SendPointsTransfer(
            new DistributorToResellerSendPointsTransferRequest
            {
                InvoiceLineItemId = response.QueueRecord.InvoiceLineItemId,
                OrganizationId = response.QueueRecord.Organization.Id,
                APIKey = response.QueueRecord.Organization.APIKey,
                SoftwareType = response.QueueRecord.SoftwareType,
                UserId = response.QueueRecord.UserId,
                Password = response.QueueRecord.Password,
                AccountId = response.QueueRecord.AccountId,
                Points = response.QueueRecord.Points,
            });

        response.QueueRecord.DateTimeProcessStarted = this.dateTimeService.UtcNow;

        return response;
    }
}