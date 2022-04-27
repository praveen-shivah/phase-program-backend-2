namespace InvoiceRepository
{
    using InvoiceRepositoryTypes;

    public interface IVendorToOperatorSendPointsTransfer
    {
        Task<VendorToOperatorSendPointsTransferResponse> SendPointsTransfer(VendorToOperatorSendPointsTransferRequest request);
    }
}
