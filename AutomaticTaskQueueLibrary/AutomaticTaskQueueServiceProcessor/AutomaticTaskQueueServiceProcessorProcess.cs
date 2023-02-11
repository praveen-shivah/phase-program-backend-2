namespace AutomaticTaskQueueLibrary;

using ApiDTO;

using AutomaticTaskSharedLibrary;

using CommonServices;

using DatabaseContext;

using InvoiceRepository;

using LoggingLibrary;

public class AutomaticTaskQueueServiceProcessorProcess : IAutomaticTaskQueueServiceProcessor
{
    private readonly IAutomaticTaskQueueServiceProcessor automaticTaskQueueServiceProcessor;

    private readonly IDistributorToOperatorSendPointsTransfer distributorToOperatorSendPointsTransfer;

    private readonly ILogger logger;

    private readonly IDateTimeService dateTimeService;

    public AutomaticTaskQueueServiceProcessorProcess(IAutomaticTaskQueueServiceProcessor automaticTaskQueueServiceProcessor,
                                                     IDistributorToOperatorSendPointsTransfer distributorToOperatorSendPointsTransfer,
                                                     ILogger logger,
                                                     IDateTimeService dateTimeService)
    {
        this.automaticTaskQueueServiceProcessor = automaticTaskQueueServiceProcessor;
        this.distributorToOperatorSendPointsTransfer = distributorToOperatorSendPointsTransfer;
        this.logger = logger;
        this.dateTimeService = dateTimeService;
    }

    async Task<AutomaticTaskQueueServiceProcessorResponse> IAutomaticTaskQueueServiceProcessor.AutomaticTaskQueueServiceProcessorAsync(
        DataContext context,
        AutomaticTaskQueueServiceProcessorRequest request)
    {
        var response = await this.automaticTaskQueueServiceProcessor.AutomaticTaskQueueServiceProcessorAsync(context, request);
        if (!response.IsSuccessful || response.QueueRecord == null || response.InvoiceLineItemRecord == null)
        {
            return response;
        }

        response.QueueRecord.DateTimeProcessStarted = this.dateTimeService.UtcNow;
        var sendPointsResponse = await this.distributorToOperatorSendPointsTransfer.SendPointsTransfer(
                                     new DistributorToResellerSendPointsTransferRequestDto
                                         {
                                             InvoiceLineItemId = response.QueueRecord.InvoiceLineItemId,
                                             OrganizationId = response.QueueRecord.Organization.Id,
                                             APIKey = response.QueueRecord.Organization.Apikey,
                                             SoftwareType = (SoftwareTypeEnum)response.QueueRecord.SoftwareType,
                                             UserId = response.QueueRecord.UserId,
                                             Password = response.QueueRecord.Password,
                                             AccountId = response.QueueRecord.AccountId,
                                             Points = response.QueueRecord.Points,
                                         });

        this.logger.Info(LogClass.CommRest, $"AutomaticTaskQueueServiceProcessorProcess Id {response.QueueRecord.Id}  IsSuccessful: {sendPointsResponse.IsSuccessful}");
        if (sendPointsResponse.IsSuccessful)
        {
            response.QueueRecord.DateTimeSent = this.dateTimeService.UtcNow;
            response.InvoiceLineItemRecord.DateTimeProcessStarted = this.dateTimeService.UtcNow;
        }
        else
        {
            response.QueueRecord.DateTimeProcessStarted = null;
        }

        return response;
    }
}