namespace AuthenticationRepository
{
    using APISupportTypes;

    using System.Threading.Tasks;

    using ISAccountRequestDto = APISupportTypes.ISAccountRequestDto;
    using ISAccountResponseDto = APISupportTypes.ISAccountResponseDto;
    using ISAccountUpdateRequestDto = APISupportTypes.ISAccountUpdateRequestDto;
    using ISAccountUpdateResponseDto = APISupportTypes.ISAccountUpdateResponseDto;
    using ISAuthenticateRequestDto = APISupportTypes.ISAuthenticateRequestDto;
    using ISAuthenticateResponseDto = APISupportTypes.ISAuthenticateResponseDto;
    using ISLogoutRequestDto = APISupportTypes.ISLogoutRequestDto;
    using ISLogoutResponseDto = APISupportTypes.ISLogoutResponseDto;
    using ISRefreshTokenRequestDto = APISupportTypes.ISRefreshTokenRequestDto;
    using ISRefreshTokenResponseDto = APISupportTypes.ISRefreshTokenResponseDto;

    public class IdentityServer : IIdentityServer
    {
        private string baseUrl = "http://localhost/";

        private readonly IRestServices<ISAuthenticateRequestDto, ISAuthenticateResponseDto> isAuthenticate;

        private readonly IRestServices<ISLogoutRequestDto, ISLogoutResponseDto> isLogout;

        private readonly IRestServices<ISRefreshTokenRequestDto, ISRefreshTokenResponseDto> isRefreshTokenRequestDto;

        private readonly IRestServices<ISAccountRequestDto, ISAccountResponseDto> isAccountRequest;

        private readonly IRestServices<ISAccountUpdateRequestDto, ISAccountUpdateResponseDto> isAccountUpdateRequest;

        public IdentityServer(IIdentityServerUrl identityServerUrl,
                              IRestServices<ISAuthenticateRequestDto, ISAuthenticateResponseDto> isAuthenticate,
                              IRestServices<ISLogoutRequestDto, ISLogoutResponseDto> isLogout,
                              IRestServices<ISRefreshTokenRequestDto, ISRefreshTokenResponseDto> isRefreshTokenRequestDto,
                              IRestServices<ISAccountRequestDto, ISAccountResponseDto> isAccountRequest,
                              IRestServices<ISAccountUpdateRequestDto, ISAccountUpdateResponseDto> isAccountUpdateRequest)
        {
            this.isAuthenticate = isAuthenticate;
            this.isLogout = isLogout;
            this.isRefreshTokenRequestDto = isRefreshTokenRequestDto;
            this.isAccountRequest = isAccountRequest;
            this.isAccountUpdateRequest = isAccountUpdateRequest;
            this.baseUrl = identityServerUrl.GetURL();
        }

        async Task<ISAuthenticateResponseDto> IIdentityServer.Authenticate(ISAuthenticateRequestDto authenticateRequestDto)
        {
            return await this.isAuthenticate.Post($"{this.baseUrl}auth/authenticate", authenticateRequestDto);
        }

        async Task<ISLogoutResponseDto> IIdentityServer.Logout(ISLogoutRequestDto isLogoutRequestDto)
        {
            return await this.isLogout.Post($"{this.baseUrl}auth/logout", isLogoutRequestDto);
        }

        async Task<ISRefreshTokenResponseDto> IIdentityServer.RefreshToken(ISRefreshTokenRequestDto isRefreshTokenRequestDto)
        {
            return await this.isRefreshTokenRequestDto.Post($"{this.baseUrl}auth/refresh-token", isRefreshTokenRequestDto);
        }

        async Task<ISAccountResponseDto> IIdentityServer.GetUserByUserName(string jwtTokenString, ISAccountRequestDto isAccountRequestDto)
        {
            return await this.isAccountRequest.Post($"{this.baseUrl}auth/get-user", jwtTokenString, isAccountRequestDto);
        }

        async Task<ISAccountUpdateResponseDto> IIdentityServer.UpdateUser(string jwtTokenString, ISAccountUpdateRequestDto isAccountUpdateRequestDto)
        {
            return await this.isAccountUpdateRequest.Post($"{this.baseUrl}auth/update-user", jwtTokenString, isAccountUpdateRequestDto);
        }
    }
}
