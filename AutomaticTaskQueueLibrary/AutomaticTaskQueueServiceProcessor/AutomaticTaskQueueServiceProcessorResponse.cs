namespace AutomaticTaskQueueLibrary;

using DatabaseContext;

public class AutomaticTaskQueueServiceProcessorResponse
{
    public bool IsSuccessful { get; set; }

    public TransferPointsQueue? QueueRecord { get; set; }

    public InvoiceLineItem? InvoiceLineItemRecord { get; set; }
}