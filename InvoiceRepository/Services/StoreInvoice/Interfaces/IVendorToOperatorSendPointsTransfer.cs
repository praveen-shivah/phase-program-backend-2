namespace InvoiceRepository
{
    using AutomaticTaskLibrary;

    using InvoiceRepositoryTypes;

    public interface IVendorToOperatorSendPointsTransfer
    {
        Task<VendorToOperatorSendPointsTransferResponse> SendPointsTransfer(VendorToOperatorSendPointsTransferRequest request);
    }
}
