namespace PlayersRepository
{
    using DatabaseContext;

    using PlayersRepositoryTypes;

    public class CreatePlayerStart : ICreatePlayer
    {
        Task<CreatePlayerResponse> ICreatePlayer.CreatePlayerAsync(DataContext dataContext, CreatePlayerRequest request)
        {
            return Task.FromResult(new CreatePlayerResponse() { IsSuccessful = true });
        }
    }
}
