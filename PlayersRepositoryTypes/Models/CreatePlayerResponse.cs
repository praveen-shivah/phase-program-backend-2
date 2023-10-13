namespace PlayersRepositoryTypes
{
    using DatabaseContext;

    public class CreatePlayerResponse
    {
        public bool IsSuccessful { get; set; }

        public Players Players { get; set; }
    }
}
