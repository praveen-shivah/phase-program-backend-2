namespace InvoiceRepository
{
    using AutomaticTaskLibrary;

    using InvoiceRepositoryTypes;

    using NServiceBus;

    public interface IVendorToOperatorSendPointsTransfer
    {
        Task<VendorToOperatorSendPointsTransferResponse> SendPointsTransfer(VendorToOperatorSendPointsTransferRequest request);
    }
}
