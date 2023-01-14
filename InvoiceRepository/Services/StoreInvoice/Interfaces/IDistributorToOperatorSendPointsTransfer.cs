namespace InvoiceRepository
{
    using AutomaticTaskSharedLibrary;

    public interface IDistributorToOperatorSendPointsTransfer
    {
        Task<DistributorToOperatorSendPointsTransferResponseDto> SendPointsTransfer(DistributorToResellerSendPointsTransferRequestDto requestDto);
    }
}