namespace AuthenticationRepository
{
    using APISupportTypes;

    using AuthenticationRepositoryTypes;

    using DatabaseContext;

    using LoggingLibrary;

    public class UpdateUserUpdateIdentityServer : IUpdateUser
    {
        private readonly IUpdateUser updateUser;

        private readonly ILogger logger;

        private readonly IIdentityServer identityServer;

        public UpdateUserUpdateIdentityServer(IUpdateUser updateUser, ILogger logger, IIdentityServer identityServer)
        {
            this.updateUser = updateUser;
            this.logger = logger;
            this.identityServer = identityServer;
        }

        async Task<UpdateUserResponse> IUpdateUser.Update(DataContext dataContext, UpdateUserRequest updateUserRequest)
        {
            var response = await this.updateUser.Update(dataContext, updateUserRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }
            this.logger.Info(LogClass.CommRest, $"Calling Identity Server for user: {updateUserRequest.UpdateUserRequestDto.UserName}");
            var result = await this.identityServer.UpdateUser(
                             updateUserRequest.JwtToken,
                             new ISAccountUpdateRequestDto()
                             {
                                 UserName = updateUserRequest.UpdateUserRequestDto.UserName,
                                 Password = updateUserRequest.UpdateUserRequestDto.Password,
                                 OrganizationId = updateUserRequest.OrganizationId,
                                 Claims = updateUserRequest.UpdateUserRequestDto.Claims
                             });

            this.logger.Info(LogClass.CommRest, $"Identity Server results for user: {updateUserRequest.UpdateUserRequestDto.UserName}");

            response.IsSuccessful = result.IsSuccessful;
            response.ResponseTypeEnum = result.ResponseTypeEnum;
            response.ErrorMessage = result.ErrorMessage;
            response.HttpStatusCode = result.HttpStatusCode;

            return response;
        }
    }
}
