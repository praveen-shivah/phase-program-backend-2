namespace ApiHost
{
    using System;
    using System.Threading.Tasks;

    using ApiDTO;

    using ApiHost.Middleware;

    using AuthenticationRepository;

    using AuthenticationRepositoryTypes;

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

        public AuthenticationController(
            ILogger logger,
            ISecretKeyRetrieval secretKeyRetrieval,
            IAuthenticationRepository authenticationRepository)
        {
            this.logger = logger;
            this.secretKeyRetrieval = secretKeyRetrieval;
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
                    accessToken = result.RefreshToken.Token,
                    roles = result.Roles.ToArray()
                };

                this.setTokenCookie(result.RefreshToken.Token);
                return this.Ok(response);
            }

            return this.StatusCode(500, 0);
        }

        [AllowAnonymous]
        [HttpGet("refresh-token")]
        public async Task<ActionResult<string>> RefreshToken()
        {
            this.logger.Debug(LogClass.General, "RefreshToken received");

            var refreshToken = this.Request.Cookies["refreshToken"];
            if (refreshToken == null)
            {
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
                    return this.Unauthorized("Invalid Refresh Token.");
                case RefreshTokenResponseType.expired:
                    return this.Unauthorized("Token expired.");
                default:
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
            // append cookie with refresh token to the http response
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(this.secretKeyRetrieval.GetRefreshTokenTTLInDays())
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }
    }
}