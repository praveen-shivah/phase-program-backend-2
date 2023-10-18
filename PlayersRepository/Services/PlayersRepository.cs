namespace PlayersRepository
{
    using ApiDTO;

    using DatabaseContext;

    using LoggingLibrary;

    using Microsoft.EntityFrameworkCore;

    using UnitOfWorkTypesLibrary;

    using PlayersRepositoryTypes;

    public class PlayersRepository : IPlayersRepository
    {
        private readonly IUnitOfWorkFactory<DataContext> unitOfWorkFactory;

        private readonly ICreatePlayer createPlayer;

        private readonly ILogger logger;
        public PlayersRepository(IUnitOfWorkFactory<DataContext> unitOfWorkFactory, ICreatePlayer createPlayer, ILogger logger)
        {
            this.unitOfWorkFactory = unitOfWorkFactory;
            this.createPlayer = createPlayer;
            this.logger = logger;
        }

        async Task<CreatePlayerResponse> IPlayersRepository.AddPlayerRequestAsync(PlayerDto playerDto)
        {
            try
            {
                var uow = this.unitOfWorkFactory.Create(
                    async context =>
                    {
                        var savePlayersResponse = await this.createPlayer.CreatePlayerAsync(context, new CreatePlayerRequest(playerDto));
                        if (savePlayersResponse.IsSuccessful)
                        {
                            return WorkItemResultEnum.doneContinue;
                        }
                        this.logger.Error(
                                LogClass.General,
                                "PlayersRepository",
                                "AddPlayerRequestAsync",
                                $"Error storing players information - reason: {savePlayersResponse}",
                                new Exception($"Error storing players information - reason: {savePlayersResponse}"));

                        return WorkItemResultEnum.rollbackExit;
                    });
                var result = await uow.ExecuteAsync();

                if (result != WorkItemResultEnum.commitSuccessfullyCompleted)
                {
                    return new CreatePlayerResponse() { IsSuccessful = false };
                }
                return new CreatePlayerResponse() { IsSuccessful = true };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        async Task<List<PlayerDto>> IPlayersRepository.GetPlayers(int softwareType)
        {
            var result = new List<PlayerDto>();
            var uow = this.unitOfWorkFactory.Create(
                async context =>
                {
                    var players = await context.Players.Where(p => p.VendorId == softwareType).ToListAsync();
                    result.Add(new PlayerDto() { IsPlaceHolder = true });
                    foreach (var player in players)
                    {
                        result.Add(
                            new PlayerDto()
                            {
                                PlayerId = player.PlayerId,
                                MobileId = player.MobileId,
                                Name = player.Name,
                                Gender = player.Gender,
                                Phone = player.Phone,
                                Mail = player.Mail,
                                LoginUsername = player.LoginUsername,
                                Balance = player.Balance.ToString(),
                                OrganizationId = player.OrganizationId,
                                ResellerId = player.ResellerId,
                                VendorId = player.VendorId
                            });
                    }

                    return WorkItemResultEnum.doneContinue;
                });
            var response = await uow.ExecuteAsync();
            return response == WorkItemResultEnum.commitSuccessfullyCompleted ? result : new List<PlayerDto>();
        }
    }
}
