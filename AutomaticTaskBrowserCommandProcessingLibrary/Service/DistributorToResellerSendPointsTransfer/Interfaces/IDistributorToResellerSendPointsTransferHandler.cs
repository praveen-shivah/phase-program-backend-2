using AutomaticTaskSharedLibrary;

namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    public interface IDistributorToResellerSendPointsTransferHandler
    {
        Task<bool> Execute(ResellerBalanceRetrieveRequestDto resellerBalanceRetrieveRequestDto);
    }
}
