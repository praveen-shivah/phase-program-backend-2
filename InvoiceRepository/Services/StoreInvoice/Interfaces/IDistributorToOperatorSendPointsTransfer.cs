namespace InvoiceRepository
{
    using AutomaticTaskSharedLibrary;

    using InvoiceRepositoryTypes;

    public interface IDistributorToOperatorSendPointsTransfer
    {
        Task<DistributorToOperatorSendPointsTransferResponse> SendPointsTransfer(DistributorToResellerSendPointsTransferRequest request);
    }
}