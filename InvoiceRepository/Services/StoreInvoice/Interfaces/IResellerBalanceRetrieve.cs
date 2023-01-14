namespace InvoiceRepository
{
    using AutomaticTaskSharedLibrary;

    using InvoiceRepositoryTypes;

    public interface IResellerBalanceRetrieve
    {
        Task<ResellerBalanceRetrieveResponseDto> GetBalance(ResellerBalanceRetrieveRequestDto requestDto);
    }
}
