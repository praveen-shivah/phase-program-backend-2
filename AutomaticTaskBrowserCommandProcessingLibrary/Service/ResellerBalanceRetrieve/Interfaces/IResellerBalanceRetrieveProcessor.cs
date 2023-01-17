using AutomaticTaskSharedLibrary;

using InvoiceRepositoryTypes;

namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    public interface IResellerBalanceRetrieveProcessor
    {
        Task<ResellerBalanceRetrieveResponseDto> Execute(ResellerBalanceRetrieveRequestDto resellerBalanceRetrieveRequestDto);
    }
}
