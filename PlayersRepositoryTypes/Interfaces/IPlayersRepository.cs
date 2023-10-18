namespace PlayersRepositoryTypes
{
    using ApiDTO;

    public interface IPlayersRepository
    {
        Task<CreatePlayerResponse> AddPlayerRequestAsync(PlayerDto playersDto);
        Task<List<PlayerDto>> GetPlayers(int softwareType);
    }
}
