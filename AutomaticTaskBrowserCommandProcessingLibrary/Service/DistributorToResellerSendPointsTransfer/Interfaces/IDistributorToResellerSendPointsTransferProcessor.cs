namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using AutomaticTaskSharedLibrary;

    public interface IDistributorToResellerSendPointsTransferProcessor
    {
        Task<bool> Execute(DistributorToResellerSendPointsTransferRequestDto distributorToResellerSendPointsTransferRequestDto);
    }
}