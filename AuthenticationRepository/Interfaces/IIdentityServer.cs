using System.Threading.Tasks;

namespace AuthenticationRepository
{
    public interface IIdentityServer
    {
        Task<ISAuthenticateResponseDto> Authenticate(ISAuthenticateRequestDto authenticateRequestDto);

        Task<ISLogoutResponseDto> Logout(ISLogoutRequestDto isLogoutRequestDto);

        Task<ISRefreshTokenResponseDto> RefreshToken(ISRefreshTokenRequestDto isRefreshTokenRequestDto);

        Task<ISAccountResponseDto> GetUserByUserName(string jwtTokenString, ISAccountRequestDto isAccountRequestDto);

        Task<ISAccountUpdateResponseDto> UpdateUser(string jwtTokenString, ISAccountUpdateRequestDto isAccountUpdateRequestDto);
    }
}
