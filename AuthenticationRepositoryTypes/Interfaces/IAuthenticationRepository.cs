﻿namespace AuthenticationRepositoryTypes
{
    using ApiDTO;

    public enum CheckRefreshTokenResponseType
    {
        successful,

        notFound,

        expired
    }

    public interface IAuthenticationRepository
    {
        Task<AuthenticationResponse> Authenticate(AuthenticationRequest authenticationRequest);

        Task<CheckRefreshTokenResponse> CheckRefreshToken(string refreshToken);

        Task StoreRefreshToken(int userId, RefreshToken newRefreshToken);
    }
}
