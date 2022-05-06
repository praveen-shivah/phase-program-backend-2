namespace InvoiceRepository
{
    using AutomaticTaskLibrary;

    using InvoiceRepositoryTypes;

    public interface IDistributorToOperatorSendPointsTransfer
    {
        Task<DistributorToOperatorSendPointsTransferResponse> SendPointsTransfer(DistributorToResellerSendPointsTransferRequest request);
    }
}
