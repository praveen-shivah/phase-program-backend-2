namespace PlayersRepositoryTypes
{
    using ApiDTO;

    public class CreatePlayerRequest
    {
        public CreatePlayerRequest(PlayerDto playerDto)
        {
            this.PlayerDto = playerDto;
        }

        public PlayerDto PlayerDto { get; }
    }
}
