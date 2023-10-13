namespace PlayersRepository
{
    using DatabaseContext;

    using PlayersRepositoryTypes;

    public interface ICreatePlayer
    {
        Task<CreatePlayerResponse> CreatePlayerAsync(DataContext dataContext, CreatePlayerRequest request);
    }
}
