namespace AuthenticationRepository;

using APISupportTypes;

using AuthenticationRepositoryTypes;

using DatabaseContext;

using LoggingLibrary;

public class AuthenticateUserIdentityServer : IAuthenticateUser
{
    private readonly IAuthenticateUser authenticateUser;

    private readonly ILogger logger;

    private readonly IIdentityServer identityServer;

    public AuthenticateUserIdentityServer(IAuthenticateUser authenticateUser, ILogger logger, IIdentityServer identityServer )
    {
        this.authenticateUser = authenticateUser;
        this.logger = logger;
        this.identityServer = identityServer;
    }

    async Task<AuthenticateUserResponse> IAuthenticateUser.AuthenticateUserAsync(DataContext context, AuthenticateUserRequest request)
    {
        var response = await this.authenticateUser.AuthenticateUserAsync(context, request);
        if (!response.IsSuccessful)
        {
            return response;
        }

        if (!string.IsNullOrEmpty(request.UserName) && !string.IsNullOrEmpty(request.Password)
                                                && !string.IsNullOrEmpty(request.Audience))
        {
            this.logger.Info(LogClass.CommRest, $"Calling Identity Server for user: {request.UserName}");
            var result = await this.identityServer.Authenticate(
                             new ISAuthenticateRequestDto
                                 {
                                     User = request.UserName,
                                     Password = request.Password,
                                     OrganizationId = request.OrganizationId,
                                     Issuer = AuthenticationConstants.REQUIRED_ISSUER,
                                     Audience = request.Audience,
                                     IPAddress = request.IpAddress
                                 });
            this.logger.Info(LogClass.CommRest, $"Identity Server results for user: {request.UserName} {result.Claims} authenticated: {result.IsAuthenticated}");

            var refreshToken = string.Empty;
            if (result.RefreshTokenResponseDto != null)
            {
                refreshToken = result.RefreshTokenResponseDto.RefreshToken;
            }

            response.IsSuccessful = result.IsSuccessful;
            response.AccessToken = result.AccessToken;
            response.Claims = result.Claims;
            response.IsAuthenticated = result.IsAuthenticated;
            response.RefreshToken = refreshToken;
            response.ResponseTypeEnum = result.ResponseTypeEnum;
            response.ErrorMessage = result.ErrorMessage;
            response.HttpStatusCode = result.HttpStatusCode;
        }

        return response;
    }
}