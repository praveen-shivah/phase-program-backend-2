using AutomaticTaskSharedLibrary;

namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    public interface IResellerBalanceRetrieveProcessor
    {
        Task<bool> Execute(ResellerBalanceRetrieveRequestDto resellerBalanceRetrieveRequestDto);
    }
}
