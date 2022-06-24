namespace AuthenticationRepository
{
    using ApiDTO;

    using AuthenticationRepositoryTypes;

    using DataPostgresqlLibrary;

    using Microsoft.EntityFrameworkCore;

    using UnitOfWorkTypesLibrary;

    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly IAuthenticateUser authenticateUser;

        private readonly IRefreshToken refreshToken;

        private readonly ILogout logout;

        private readonly IUpdateUser updateUser;

        private readonly IUnitOfWorkFactory<DPContext> unitOfWorkFactory;

        public AuthenticationRepository(
            IUnitOfWorkFactory<DPContext> unitOfWorkFactory,
            IAuthenticateUser authenticateUser,
            IRefreshToken refreshToken,
            ILogout logout,
            IUpdateUser updateUser)
        {
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
                        authenticateUserResponse = await this.authenticateUser.Authenticate(
                                                       context,
                                                       new AuthenticateUserRequest
                                                       {
                                                           UserName = authenticationRequest.UserId,
                                                           Password = authenticationRequest.Password,
                                                           IpAddress = authenticationRequest.IpAddress
                                                       });

                        return WorkItemResultEnum.doneContinue;
                    });

            var result = await uow.ExecuteAsync();
            if (result != WorkItemResultEnum.commitSuccessfullyCompleted || authenticateUserResponse == null)
            {
                return new AuthenticationResponse
                {
                    IsSuccessful = false,
                    IsAuthenticated = false
                };
            }

            if (authenticateUserResponse.IsSuccessful && authenticateUserResponse.IsAuthenticated)
            {
                return new AuthenticationResponse
                {
                    UserId = authenticateUserResponse.UserId,
                    UserName = authenticateUserResponse.UserName,
                    IsAuthenticated = authenticateUserResponse.IsAuthenticated,
                    IsSuccessful = true,
                    Roles = authenticateUserResponse.Roles,
                    JwtToken = authenticateUserResponse.JwtToken,
                    RefreshToken = new RefreshTokenDto()
                    {
                        Created = authenticateUserResponse.RefreshToken.Created,
                        CreatedByIp = authenticateUserResponse.RefreshToken.CreatedByIp,
                        Expires = authenticateUserResponse.RefreshToken.Expires,
                        Token = authenticateUserResponse.RefreshToken.Token
                    }
                };
            }

            return new AuthenticationResponse
            {
                UserId = authenticateUserResponse.UserId,
                UserName = authenticateUserResponse.UserName,
                IsAuthenticated = authenticateUserResponse.IsAuthenticated,
                IsSuccessful = true,
                Roles = authenticateUserResponse.Roles
            };
        }

        async Task<UpdateUserResponse> IAuthenticationRepository.UpdateUser(UserDto userDto)
        {
            var uow = this.unitOfWorkFactory.Create(
                async context =>
                    {
                        await this.updateUser.Update(context, new UpdateUserRequest(userDto));

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
                        foreach (var user in list)
                        {
                            userList.Add(new UserDto(){Id = user.Id, Email = user.Email, UserName = user.UserName});
                        }

                        return WorkItemResultEnum.doneContinue;
                    });
            var result = await uow.ExecuteAsync();
            if (result != WorkItemResultEnum.commitSuccessfullyCompleted)
            {
                return new List<UserDto>();
            }

            return userList;
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