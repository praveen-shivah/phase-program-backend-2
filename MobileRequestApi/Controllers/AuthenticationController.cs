namespace ApiHost
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web;

    using ApiDTO;

    using ApiHost.Middleware;

    using APISupport;

    using APISupportTypes;

    using AuthenticationRepository;

    using AuthenticationRepositoryTypes;

    using CommonServices;

    using LoggingLibrary;

    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    using SecurityUtilitiesTypes;

    using IIdentityServer = AuthenticationRepository.IIdentityServer;

    [AuthorizePolicy]
    [ApiController]
    [Route("api/authentication")]
    [EnableCors("allowLocalHostOrigins")]
    public class AuthenticationController : ApiControllerBase
    {
        private readonly IAuthenticationRepository authenticationRepository;

        private readonly IIdentityServer identityServer;

        private readonly IDateTimeService dateTimeService;

        private readonly ILogger logger;

        private readonly ISecretKeyRetrieval secretKeyRetrieval;

        public AuthenticationController(
            ILogger logger,
            ISecretKeyRetrieval secretKeyRetrieval,
            IDateTimeService dateTimeService,
            IAuthenticationRepository authenticationRepository,
            IIdentityServer identityServer)
        {
            this.logger = logger;
            this.secretKeyRetrieval = secretKeyRetrieval;
            this.dateTimeService = dateTimeService;
            this.authenticationRepository = authenticationRepository;
            this.identityServer = identityServer;
        }

        [AllowAnonymous]
        [HttpPost("admin-login")]
        public async Task<ActionResult<AuthenticateResponseDto>> AdminLogin(AuthenticateRequestDto authenticateDto)
        {
            this.logger.Info(LogClass.General, "AdminLogin received");
            if (string.IsNullOrWhiteSpace(authenticateDto.audience))
            {
                authenticateDto.audience = this.HttpContext.Session.Id;
            }
            var result = await this.authenticationRepository.Authenticate(new AuthenticationRequest(this.OrganizationId, authenticateDto.user, authenticateDto.pwd, this.ipAddress(), authenticateDto.audience));
            this.logger.Info(LogClass.General, "AdminLogin authenticate returned");

            if (result.IsSuccessful && result.IsAuthenticated)
            {
                this.logger.Info(LogClass.General, "AdminLogin authenticate returned success");
                var response = new AuthenticateResponseDto
                {
                    OrganizationId = result.OrganizationId,
                    IsAuthenticated = true,
                    AccessToken = result.AccessToken,
                    Roles = result.Roles.ToArray(),
                    ErrorMessage = result.ErrorMessage,
                    HttpStatusCode = result.HttpStatusCode,
                    IsSuccessful = true,
                    ResponseTypeEnum = result.ResponseTypeEnum
                };

                this.setTokenCookie(result.RefreshToken);
                return this.Ok(response);
            }

            if (result.IsSuccessful && !result.IsAuthenticated)
            {
                var response2 = new AuthenticateResponseDto
                {
                    OrganizationId = result.OrganizationId,
                    IsAuthenticated = false,
                    AccessToken = string.Empty,
                    Roles = result.Roles.ToArray(),
                    ErrorMessage = result.ErrorMessage,
                    HttpStatusCode = result.HttpStatusCode,
                    IsSuccessful = false,
                    ResponseTypeEnum = result.ResponseTypeEnum
                };

                this.logger.Info(LogClass.General, "AdminLogin authenticate returned success but not authenticated");
                return this.StatusCode((int)HttpStatusCode.Forbidden, response2);
            }

            var response3 = new AuthenticateResponseDto
            {
                OrganizationId = result.OrganizationId,
                IsAuthenticated = false,
                AccessToken = string.Empty,
                Roles = result.Roles.ToArray(),
                ErrorMessage = result.ErrorMessage,
                HttpStatusCode = result.HttpStatusCode,
                IsSuccessful = false,
                ResponseTypeEnum = result.ResponseTypeEnum
            };

            this.logger.Info(LogClass.General, "AdminLogin authenticate returned - but not successful");
            return this.StatusCode(500, response3);
        }

        [AllowAnonymous]
        [HttpGet("health-check")]
        public IActionResult Get()
        {
            return this.Ok();
        }

        [HttpGet("get-users")]
        [AuthorizePolicy(Policy = AuthenticationConstants.POLICY_ADMIN)]
        public async Task<ActionResult<List<UpdateUserRequestDto>>> GetUsers()
        {
            this.logger.Debug(LogClass.General, "GetUsers received");

            var result = await this.authenticationRepository.GetUsers();
            return this.Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            this.logger.Debug(LogClass.CommRest, "Logout received");
            this.setTokenCookie(string.Empty);

            var userName = this.HttpContext?.User?.Identity?.Name;
            var result = await this.identityServer.Logout(new ISLogoutRequestDto { UserName = userName });
            if (result.IsSuccessful)
            {
                if (this.HttpContext != null)
                {
                    await this.HttpContext.SignOutAsync();
                }

                return this.Ok();
            }

            return this.StatusCode((int)HttpStatusCode.InternalServerError, 0);
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> RefreshToken()
        {
            this.logger.Debug(LogClass.CommRest, "RefreshToken received");

            var refreshToken = HttpUtility.UrlDecode(this.Request.Headers["Refresh-Token"]);
            if (string.IsNullOrEmpty(refreshToken))
            {
                refreshToken = HttpUtility.UrlDecode(this.Request.Cookies["refreshToken"]);
                if (string.IsNullOrEmpty(refreshToken))
                {
                    this.setTokenCookie(string.Empty);
                    return this.Unauthorized("Refresh Token missing.");
                }
            }

            var result = await this.identityServer.RefreshToken(new ISRefreshTokenRequestDto
            {
                RefreshToken = refreshToken,
                IPAddress = this.ipAddress(),
                Issuer = AuthenticationConstants.REQUIRED_ISSUER
            });

            switch (result.RefreshTokenDtoType)
            {
                case RefreshTokenDtoType.successful:
                    this.setTokenCookie(result.RefreshToken);
                    return this.Ok(result.JwtToken);
                case RefreshTokenDtoType.notFound:
                    this.setTokenCookie(string.Empty);
                    return this.Unauthorized("Invalid Refresh Token.");
                case RefreshTokenDtoType.expired:
                    this.setTokenCookie(string.Empty);
                    return this.Unauthorized("Token expired.");
                case RefreshTokenDtoType.currentTokenNotFound:
                    this.setTokenCookie(string.Empty);
                    return this.Unauthorized("Invalid Refresh Token.");
                case RefreshTokenDtoType.attemptedReuse:
                case RefreshTokenDtoType.notActive:
                case RefreshTokenDtoType.duplicated:
                default:
                    this.setTokenCookie(string.Empty);
                    return this.Unauthorized("Invalid Refresh Token.");
            }
        }

        [HttpPost("update-user")]
        [AuthorizePolicy(Policy = AuthenticationConstants.POLICY_ADMIN)]
        public async Task<ActionResult<UpdateUserResponseDto>> UpdateUser(UpdateUserRequestDto updateUserRequestDto)
        {
            this.logger.Debug(LogClass.General, "UpdateUser received");
            var jwtTokenString = this.HttpContext.Request.Headers["Access-Token"].FirstOrDefault()?.Split(' ').Last();
            var result = await this.authenticationRepository.UpdateUser(jwtTokenString, this.OrganizationId, updateUserRequestDto);
            var response = new UpdateUserResponseDto()
            {
                ErrorMessage = result.ErrorMessage,
                HttpStatusCode = result.HttpStatusCode,
                IsSuccessful = result.IsSuccessful,
                ResponseTypeEnum = result.ResponseTypeEnum
            };
            return this.Ok(response);
        }

        private string ipAddress()
        {
            string? result;
            // get source ip address for the current requestDto
            if (this.Request.Headers.ContainsKey("X-Forwarded-For"))
            {
                result = this.Request.Headers["X-Forwarded-For"];
            }
            else
            {
                result = this.HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString();
            }

            return result ?? string.Empty;
        }

        private void setTokenCookie(string token)
        {
            var expires = this.dateTimeService.UtcNow.AddDays(this.secretKeyRetrieval.GetRefreshTokenTTLInDays());
            if (string.IsNullOrEmpty(token))
            {
                expires = this.dateTimeService.UtcNow.AddDays(-1D);
            }

            // append cookie with refresh token to the http response
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                IsEssential = true,
                Secure = false,
                SameSite = SameSiteMode.None,
                Domain = "localhost",
                Expires = expires.AddMinutes(15)
            };

            this.Response.Cookies.Append("refreshToken", token, cookieOptions);
        }
    }
}