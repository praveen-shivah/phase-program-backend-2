namespace PlayersRepositoryTypes
{
    using ApiDTO;

    public interface IPlayersRepository
    {
        Task<CreatePlayerResponse> AddPlayerRequestAsync(PlayerDto playersDto);
    }
}
