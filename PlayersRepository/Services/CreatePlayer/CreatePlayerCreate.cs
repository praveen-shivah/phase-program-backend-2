namespace PlayersRepository
{
    using DatabaseContext;

    using Microsoft.EntityFrameworkCore;

    using PlayersRepositoryTypes;

    public class CreatePlayerCreate : ICreatePlayer
    {
        private readonly ICreatePlayer createPlayer;

        public CreatePlayerCreate(ICreatePlayer createPlayer)
        {
            this.createPlayer = createPlayer;
        }
        async Task<CreatePlayerResponse> ICreatePlayer.CreatePlayerAsync(DataContext dataContext, CreatePlayerRequest request)
        {
            try
            {
                var response = await this.createPlayer.CreatePlayerAsync(dataContext, request);
                if (!response.IsSuccessful)
                {
                    return response;
                }
                //Check if Player exist and update the data based on playerId
                var existingPlayer = await dataContext.Players
                    .SingleOrDefaultAsync(p => p.PlayerId == request.PlayerDto.PlayerId);

                if (existingPlayer != null)
                {
                    existingPlayer.MobileId = request.PlayerDto.MobileId;
                    existingPlayer.Name = request.PlayerDto.Name;
                    existingPlayer.Gender = request.PlayerDto.Gender;
                    existingPlayer.Phone = request.PlayerDto.Phone;
                    existingPlayer.Mail = request.PlayerDto.Mail;
                    existingPlayer.CreatedOn = DateTime.UtcNow;
                    existingPlayer.ModifiedOn = DateTime.UtcNow;
                    existingPlayer.OrganizationId = request.PlayerDto.OrganizationId;
                    existingPlayer.ResellerId = request.PlayerDto.ResellerId;
                    existingPlayer.VendorId = request.PlayerDto.VendorId;
                    existingPlayer.LoginUsername = request.PlayerDto.LoginUsername;
                    existingPlayer.LoginPassword = request.PlayerDto.LoginPassword;

                    await dataContext.SaveChangesAsync();

                    return new CreatePlayerResponse { IsSuccessful = true };
                }
                // Add a new Player
                var players = new Players()
                {
                    PlayerId = request.PlayerDto.PlayerId,
                    MobileId = request.PlayerDto.MobileId,
                    Name = request.PlayerDto.Name,
                    Gender = request.PlayerDto.Gender,
                    Phone = request.PlayerDto.Phone,
                    Mail = request.PlayerDto.Mail,
                    CreatedOn = DateTime.UtcNow,
                    ModifiedOn = DateTime.UtcNow,
                    OrganizationId = request.PlayerDto.OrganizationId,
                    ResellerId = request.PlayerDto.ResellerId,
                    VendorId = request.PlayerDto.VendorId,
                    LoginUsername = request.PlayerDto.LoginUsername,
                    LoginPassword = request.PlayerDto.LoginPassword,
                };

                await dataContext.Players.AddAsync(players);
                await dataContext.SaveChangesAsync();

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

