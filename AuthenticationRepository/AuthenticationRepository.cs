namespace AuthenticationRepository
{
    using ApiDTO;

    using AuthenticationRepositoryTypes;

    using DataPostgresqlLibrary;

    using UnitOfWorkTypesLibrary;

    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly IUnitOfWorkFactory<DPContext> unitOfWorkFactory;

        private readonly IAuthenticateUser authenticateUser;

        private readonly IStoreRefreshToken storeRefreshToken;

        private readonly ICheckRefreshToken checkRefreshToken;

        public AuthenticationRepository(IUnitOfWorkFactory<DPContext> unitOfWorkFactory, IAuthenticateUser authenticateUser, IStoreRefreshToken storeRefreshToken, ICheckRefreshToken checkRefreshToken)
        {
            this.unitOfWorkFactory = unitOfWorkFactory;
            this.authenticateUser = authenticateUser;
            this.storeRefreshToken = storeRefreshToken;
            this.checkRefreshToken = checkRefreshToken;
        }

        async Task<AuthenticationResponse> IAuthenticationRepository.Authenticate(AuthenticationRequest authenticationRequest)
        {
            AuthenticateUserResponse authenticateUserResponse = null;
            var uow = this.unitOfWorkFactory.Create(
                async context =>
                    {
                        authenticateUserResponse = await this.authenticateUser.Authenticate(
                                                       context,
                                                       new AuthenticateUserRequest()
                                                       {
                                                           UserName = authenticationRequest.UserId,
                                                           Password = authenticationRequest.Password
                                                       });

                        return WorkItemResultEnum.doneContinue;
                    });

            var result = await uow.ExecuteAsync();
            if (result != WorkItemResultEnum.commitSuccessfullyCompleted || authenticateUserResponse == null)
            {
                return new AuthenticationResponse()
                {
                    IsSuccessful = false,
                    IsAuthenticated = false
                };
            }

            return new AuthenticationResponse()
            {
                UserId = authenticateUserResponse.UserId,
                UserName = authenticateUserResponse.UserName,
                IsAuthenticated = authenticateUserResponse.IsAuthenticated,
                IsSuccessful = true
            };
        }

        async Task<CheckRefreshTokenResponse> IAuthenticationRepository.CheckRefreshToken(string refreshToken)
        {
            CheckRefreshTokenResponse checkRefreshTokenResponse = null;
            var uow = this.unitOfWorkFactory.Create(
                async context =>
                    {
                        checkRefreshTokenResponse = await this.checkRefreshToken.Check(context, new CheckRefreshTokenRequest { RefreshToken = refreshToken });
                        return WorkItemResultEnum.doneContinue;
                    });

            var result = await uow.ExecuteAsync();
            if (result != WorkItemResultEnum.commitSuccessfullyCompleted || checkRefreshTokenResponse == null)
            {
                return new CheckRefreshTokenResponse()
                {
                    IsSuccessful = false
                };
            }

            return checkRefreshTokenResponse;
        }

        async Task<StoreRefreshTokenResponse> IAuthenticationRepository.StoreRefreshToken(int userId, RefreshToken newRefreshToken)
        {
            StoreRefreshTokenResponse storeRefreshTokenResponse = null;
            var uow = this.unitOfWorkFactory.Create(
                async context =>
                    {
                        storeRefreshTokenResponse = await this.storeRefreshToken.Store(
                                                        context,
                                                        new StoreRefreshTokenRequest()
                                                        {
                                                            UserId = userId,
                                                            Token = newRefreshToken.Token,
                                                            Created = newRefreshToken.Created,
                                                            Expires = newRefreshToken.Expires
                                                        });

                        return WorkItemResultEnum.doneContinue;
                    });

            var result = await uow.ExecuteAsync();
            if (result != WorkItemResultEnum.commitSuccessfullyCompleted || storeRefreshTokenResponse == null)
            {
                return new StoreRefreshTokenResponse()
                {
                    IsSuccessful = false
                };
            }

            return storeRefreshTokenResponse;
        }
    }
}
