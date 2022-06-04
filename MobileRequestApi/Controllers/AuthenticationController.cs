namespace ApiHost.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;

    using ApiDTO;

    using AuthenticationRepositoryTypes;

    using LoggingLibrary;

    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;

    [ApiController]
    [Route("api/authentication")]
    [EnableCors("_myAllowSpecificOrigins")]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationRepository authenticationRepository;

        private readonly IConfiguration configuration;

        private readonly ILogger logger;

        public AuthenticationController(
            ILogger logger,
            IConfiguration configuration,
            IAuthenticationRepository authenticationRepository)
        {
            this.logger = logger;
            this.configuration = configuration;
            this.authenticationRepository = authenticationRepository;
        }

        [HttpPost("admin-login")]
        public async Task<ActionResult<AuthenticateResponseDto>> AdminLogin(AuthenticateRequestDto authenticateDto)
        {
            this.logger.Debug(LogClass.General, "AdminLogin received");
            var result = await this.authenticationRepository.Authenticate(new AuthenticationRequest(authenticateDto.user, authenticateDto.pwd));

            if (result.IsSuccessful && result.IsAuthenticated)
            {
                var response = new AuthenticateResponseDto
                                   {
                                       IsAuthenticated = true,
                                       accessToken = this.createToken(result.UserName),
                                       roles = new[] { 2001, 5150 }
                                   };

                var refreshToken = this.generateRefreshToken();
                await this.setRefreshToken(result.UserId, refreshToken);
                return this.Ok(response);
            }

            return this.StatusCode(500, 0);
        }

        [HttpGet("refresh-token")]
        public async Task<ActionResult<string>> RefreshToken()
        {
            this.logger.Debug(LogClass.General, "AdminLogin received");
            var refreshToken = this.Request.Cookies["refreshToken"];
            if (refreshToken == null)
            {
                return this.Unauthorized("Refresh Token missing.");
            }

            var result = await this.authenticationRepository.CheckRefreshToken(refreshToken);
            switch (result.CheckRefreshTokenResponseType)
            {
                case CheckRefreshTokenResponseType.successful:
                    var token = this.createToken(result.UserName);
                    var newRefreshToken = this.generateRefreshToken();
                    await this.setRefreshToken(result.UserId, newRefreshToken);
                    return this.Ok(token);
                case CheckRefreshTokenResponseType.notFound:
                    return this.Unauthorized("Invalid Refresh Token.");
                case CheckRefreshTokenResponseType.expired:
                    return this.Unauthorized("Token expired.");
                default:
                    return this.Unauthorized("Invalid Refresh Token.");
            }
        }

        private string createToken(string userName)
        {
            var claims = new List<Claim>
                             {
                                 new Claim(ClaimTypes.Name, userName),
                             };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration.GetSection("AppSettings:Token").Value));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddMinutes(15), signingCredentials: signingCredentials);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private RefreshToken generateRefreshToken()
        {
            var refreshToken = new RefreshToken
                                   {
                                       Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                                       Expires = DateTime.Now.AddMinutes(7),
                                       Created = DateTime.Now
                                   };

            return refreshToken;
        }

        private async Task setRefreshToken(int userId, RefreshToken newRefreshToken)
        {
            var cookieOptions = new CookieOptions
                                    {
                                        HttpOnly = true,
                                        Expires = newRefreshToken.Expires
                                    };
            this.Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);
            await this.authenticationRepository.StoreRefreshToken(userId, newRefreshToken);
        }
    }
}