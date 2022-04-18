namespace InvoiceRepository
{
    using InvoiceRepositoryTypes;

    public interface ISendPointsTransfer
    {
        SendPointsTransferResponse SendPointsTransfer(SendPointsTransferRequest request);
    }
}
