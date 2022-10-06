namespace AutomaticTaskQueueLibrary;

using DataModelsLibrary;

public class AutomaticTaskQueueServiceProcessorResponse
{
    public bool IsSuccessful { get; set; }

    public TransferPointsQueue? QueueRecord { get; set; }

    public InvoiceLineItem? InvoiceLineItemRecord { get; set; }
}