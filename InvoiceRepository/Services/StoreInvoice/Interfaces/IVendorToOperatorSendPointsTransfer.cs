namespace InvoiceRepository
{
    using InvoiceRepositoryTypes;

    public interface IVendorToOperatorSendPointsTransfer
    {
        VendorToOperatorSendPointsTransferResponse SendPointsTransfer(VendorToOperatorSendPointsTransferRequest request);
    }
}
