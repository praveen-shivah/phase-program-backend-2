namespace ApiHost
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ApiDTO;

    using ApiHost.Middleware;

    using AuthenticationRepository;

    using AuthenticationRepositoryTypes;

    using CommonServices;

    using LoggingLibrary;

    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    using SecurityUtilitiesTypes;

    [Authorize]
    [ApiController]
    [Route("api/authentication")]
    [EnableCors("_myAllowSpecificOrigins")]
    public class AuthenticationController : ApiControllerBase
    {
        private readonly IAuthenticationRepository authenticationRepository;

        private readonly ILogger logger;

        private readonly ISecretKeyRetrieval secretKeyRetrieval;

        private readonly IDateTimeService dateTimeService;

        public AuthenticationController(
            ILogger logger,
            ISecretKeyRetrieval secretKeyRetrieval,
            IDateTimeService dateTimeService,
            IAuthenticationRepository authenticationRepository)
        {
            this.logger = logger;
            this.secretKeyRetrieval = secretKeyRetrieval;
            this.dateTimeService = dateTimeService;
            this.authenticationRepository = authenticationRepository;
        }

        [AllowAnonymous]
        [HttpPost("admin-login")]
        public async Task<ActionResult<AuthenticateResponseDto>> AdminLogin(AuthenticateRequestDto authenticateDto)
        {
            this.logger.Debug(LogClass.General, "AdminLogin received");
            var result = await this.authenticationRepository.Authenticate(new AuthenticationRequest(authenticateDto.user, authenticateDto.pwd, this.ipAddress()));

            if (result.IsSuccessful && result.IsAuthenticated)
            {
                var response = new AuthenticateResponseDto
                {
                    IsAuthenticated = true,
                    accessToken = result.JwtToken,
                    roles = result.Roles.ToArray()
                };

                this.setTokenCookie(result.RefreshToken.Token);
                return this.Ok(response);
            }

            return this.StatusCode(500, 0);
        }

        [AllowAnonymous]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            this.logger.Debug(LogClass.General, "RefreshToken received");
            this.setTokenCookie(string.Empty);

            if (this.UserId <= 0)
            {
                return this.Ok();
            }

            var result = await this.authenticationRepository.Logout(new LogoutRequest(this.UserId));
            if (result.IsSuccessful)
            {
                return this.Ok();
            }

            return this.StatusCode(500, 0);
        }

        [HttpGet("get-users")]
        public async Task<ActionResult<List<UserDto>>> GetUsers()
        {
            this.logger.Debug(LogClass.General, "GetUsers received");

            var result = await this.authenticationRepository.GetUsers();
           return this.Ok(result);
        }

        [HttpPost("update-user")]
        public async Task<IActionResult> UpdateUser(UserDto userDto)
        {
            this.logger.Debug(LogClass.General, "UpdateUser received");

            var result = await this.authenticationRepository.UpdateUser(this.OrganizationId, userDto);
            return this.Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> RefreshToken()
        {
            this.logger.Debug(LogClass.General, "RefreshToken received");

            var refreshToken = this.Request.Cookies["refreshToken"];
            if (refreshToken == null)
            {
                this.setTokenCookie(string.Empty);
                return this.Unauthorized("Refresh Token missing.");
            }

            var userId = (int)(this.HttpContext.Items["UserId"] ?? 0);
            var result = await this.authenticationRepository.RefreshToken(refreshToken, userId, this.ipAddress());
            switch (result.RefreshTokenResponseType)
            {
                case RefreshTokenResponseType.successful:
                    this.setTokenCookie(result.RefreshToken.Token);
                    return this.Ok(result.JwtToken);
                case RefreshTokenResponseType.notFound:
                    this.setTokenCookie(string.Empty);
                    return this.Unauthorized("Invalid Refresh Token.");
                case RefreshTokenResponseType.expired:
                    this.setTokenCookie(string.Empty);
                    return this.Unauthorized("Token expired.");
                case RefreshTokenResponseType.currentTokenNotFound:
                    this.setTokenCookie(string.Empty);
                    return this.Unauthorized("Invalid Refresh Token.");
                case RefreshTokenResponseType.attemptedReuse:
                case RefreshTokenResponseType.notActive:
                case RefreshTokenResponseType.duplicated:
                default:
                    this.setTokenCookie(string.Empty);
                    return this.Unauthorized("Invalid Refresh Token.");
            }
        }

        private string ipAddress()
        {
            string? result;
            // get source ip address for the current request
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