namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using AutomaticTaskSharedLibrary;
    using PlayersRepositoryTypes;
    public interface IResellerPlayersRetrieveProcessor
    {
        Task<ResellerPlayersRetrieveResponseDto> Execute(ResellerPlayersRetrieveRequestDto resellerPlayersRetrieveRequestDto);
    }
}
