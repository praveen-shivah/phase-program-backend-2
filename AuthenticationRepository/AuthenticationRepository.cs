namespace AuthenticationRepository
{
    using ApiDTO;

    using AuthenticationRepositoryTypes;

    using DatabaseContext;

    using LoggingLibrary;

    using Microsoft.EntityFrameworkCore;

    using UnitOfWorkTypesLibrary;

    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly IAuthenticateUser authenticateUser;

        private readonly IRefreshToken refreshToken;

        private readonly ILogout logout;

        private readonly IUpdateUser updateUser;

        private readonly ILogger logger;

        private readonly IUnitOfWorkFactory<DataContext> unitOfWorkFactory;

        public AuthenticationRepository(
            ILogger logger,
            IUnitOfWorkFactory<DataContext> unitOfWorkFactory,
            IAuthenticateUser authenticateUser,
            IRefreshToken refreshToken,
            ILogout logout,
            IUpdateUser updateUser)
        {
            this.logger = logger;
            this.unitOfWorkFactory = unitOfWorkFactory;
            this.authenticateUser = authenticateUser;
            this.refreshToken = refreshToken;
            this.logout = logout;
            this.updateUser = updateUser;
        }

        async Task<AuthenticationResponse> IAuthenticationRepository.Authenticate(AuthenticationRequest authenticationRequest)
        {
            AuthenticateUserResponse? authenticateUserResponse = null;
            var uow = this.unitOfWorkFactory.Create(
                async context =>
                    {
                        this.logger.Info(LogClass.CommRest, "Calling authenticate user");
                        authenticateUserResponse = await this.authenticateUser.AuthenticateUserAsync(
                                                       context,
                                                       new AuthenticateUserRequest
                                                       {
                                                           UserName = authenticationRequest.UserId,
                                                           Password = authenticationRequest.Password,
                                                           IpAddress = authenticationRequest.IpAddress
                                                       });
                        this.logger.Info(LogClass.CommRest, "Calling authenticate user done");

                        return WorkItemResultEnum.doneContinue;
                    });

            try
            {
                this.logger.Info(LogClass.CommRest, "Calling authenticate user done before executeAsync()");
                var result = await uow.ExecuteAsync();
                this.logger.Info(LogClass.CommRest, "Calling authenticate user done after executeAsync()");

                if (result != WorkItemResultEnum.commitSuccessfullyCompleted || authenticateUserResponse == null)
                {
                    this.logger.Info(LogClass.CommRest, "authenticate user repository failed");
                    return new AuthenticationResponse
                    {
                        IsSuccessful = false,
                        IsAuthenticated = false
                    };
                }

                if (authenticateUserResponse.IsSuccessful && authenticateUserResponse.IsAuthenticated)
                {
                    this.logger.Info(LogClass.CommRest, "authenticate user repository success - building response");
                    return new AuthenticationResponse
                    {
                        OrganizationId = authenticateUserResponse.User.Organization.Id,
                        UserId = authenticateUserResponse.UserId,
                        UserName = authenticateUserResponse.UserName,
                        IsAuthenticated = authenticateUserResponse.IsAuthenticated,
                        IsSuccessful = true,
                        Roles = authenticateUserResponse.Roles,
                        AccessToken = authenticateUserResponse.AccessToken,
                        RefreshToken = authenticateUserResponse.RefreshToken
                    };
                }

                this.logger.Info(LogClass.CommRest, "authenticate user repository not authenticated - building response");
                return new AuthenticationResponse
                {
                    UserId = authenticateUserResponse.UserId,
                    UserName = authenticateUserResponse.UserName,
                    IsAuthenticated = authenticateUserResponse.IsAuthenticated,
                    IsSuccessful = true,
                    Roles = new List<int>()
                };
            }
            catch (Exception e)
            {
                var innerMessage = string.Empty;
                if(e.InnerException != null)
                    innerMessage = e.InnerException.Message;

                this.logger.Info(LogClass.CommRest, $"authenticate user repository error: {e.Message} {innerMessage}");
                return new AuthenticationResponse
                {
                    UserId = 0,
                    UserName = string.Empty,
                    IsAuthenticated = false,
                    IsSuccessful = false,
                    Roles = new List<int>()
                };
            }
        }

        async Task<UpdateUserResponse> IAuthenticationRepository.UpdateUser(int organizationId, UserDto userDto)
        {
            var uow = this.unitOfWorkFactory.Create(
                async context =>
                    {
                        await this.updateUser.Update(context, new UpdateUserRequest(organizationId, userDto));

                        return WorkItemResultEnum.doneContinue;
                    });
            var result = await uow.ExecuteAsync();

            if (result != WorkItemResultEnum.commitSuccessfullyCompleted)
            {
                return new UpdateUserResponse() { IsSuccessful = false };
            }

            return new UpdateUserResponse() { IsSuccessful = true };
        }

        async Task<List<UserDto>> IAuthenticationRepository.GetUsers()
        {
            var userList = new List<UserDto>();
            var uow = this.unitOfWorkFactory.Create(
                async context =>
                    {
                        var list = await context.User.ToListAsync();
                        userList.Add(new UserDto() { IsPlaceHolder = true });
                        foreach (var user in list)
                        {
                            userList.Add(new UserDto() { Id = user.Id, Email = user.Email, UserName = user.UserName, IsActive = user.IsActive });
                        }

                        return WorkItemResultEnum.doneContinue;
                    });
            var result = await uow.ExecuteAsync();
            if (result != WorkItemResultEnum.commitSuccessfullyCompleted)
            {
                return new List<UserDto>();
            }

            return userList.OrderBy(x => x.UserName).ToList();
        }

        async Task<LogoutResponse> IAuthenticationRepository.Logout(LogoutRequest logoutRequest)
        {
            var logoutResponse = new LogoutResponse() { IsSuccessful = true };
            var uow = this.unitOfWorkFactory.Create(
                async context =>
                    {
                        logoutResponse = await this.logout.Logout(context, logoutRequest);
                        return WorkItemResultEnum.doneContinue;
                    });

            var result = await uow.ExecuteAsync();
            if (result != WorkItemResultEnum.commitSuccessfullyCompleted)
            {
                return new LogoutResponse() { IsSuccessful = false };
            }

            return logoutResponse;
        }

        async Task<RefreshTokenResponse> IAuthenticationRepository.RefreshToken(string refreshToken, int userId, string ipAddress)
        {
            RefreshTokenResponse? refreshTokenResponse = null;
            var uow = this.unitOfWorkFactory.Create(
                async context =>
                    {
                        refreshTokenResponse = await this.refreshToken.Refresh(context, new RefreshTokenRequest { RefreshToken = refreshToken, IpAddress = ipAddress });
                        return WorkItemResultEnum.doneContinue;
                    });

            var result = await uow.ExecuteAsync();
            if (result != WorkItemResultEnum.commitSuccessfullyCompleted || refreshTokenResponse == null)
            {
                return new RefreshTokenResponse { IsSuccessful = false };
            }

            return refreshTokenResponse;
        }

        async Task<AuthenticationResponse> IAuthenticationRepository.GetUserById(int id)
        {
            AuthenticationResponse? authenticationResponse = null;
            var uow = this.unitOfWorkFactory.Create(
                async context =>
                    {
                        var user = await context.User.SingleOrDefaultAsync(x => x.Id == id);
                        if (user == null)
                        {
                            authenticationResponse = new AuthenticationResponse
                            {
                                IsAuthenticated = false,
                                IsSuccessful = false
                            };
                        }
                        else
                        {
                            authenticationResponse = new AuthenticationResponse
                            {
                                IsAuthenticated = true,
                                IsSuccessful = true,
                                UserId = user.Id,
                            };
                        }

                        return WorkItemResultEnum.doneContinue;
                    });

            var result = await uow.ExecuteAsync();
            if (result != WorkItemResultEnum.commitSuccessfullyCompleted || authenticationResponse == null)
            {
                return new AuthenticationResponse
                {
                    IsSuccessful = false,
                    IsAuthenticated = false
                };
            }

            return authenticationResponse;
        }
    }
}