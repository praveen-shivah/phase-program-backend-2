﻿namespace AuthenticationRepository
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

        private readonly IUnitOfWorkFactory<DPContext> unitOfWorkFactory;

        public AuthenticationRepository(
            IUnitOfWorkFactory<DPContext> unitOfWorkFactory,
            IAuthenticateUser authenticateUser,
            IRefreshToken refreshToken)
        {
            this.unitOfWorkFactory = unitOfWorkFactory;
            this.authenticateUser = authenticateUser;
            this.refreshToken = refreshToken;
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

            return new AuthenticationResponse
            {
                UserId = authenticateUserResponse.UserId,
                UserName = authenticateUserResponse.UserName,
                IsAuthenticated = authenticateUserResponse.IsAuthenticated,
                IsSuccessful = true,
                RefreshToken = new RefreshTokenDto()
                {
                    Created = authenticateUserResponse.RefreshToken.Created,
                    CreatedByIp = authenticateUserResponse.RefreshToken.CreatedByIp,
                    Expires = authenticateUserResponse.RefreshToken.Expires,
                    Token = authenticateUserResponse.JwtToken
                }
            };
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