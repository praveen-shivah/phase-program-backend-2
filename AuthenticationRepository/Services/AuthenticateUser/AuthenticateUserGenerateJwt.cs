namespace AuthenticationRepository
{
    using CommonServices;

    using Microsoft.IdentityModel.Tokens;

    using SecurityUtilitiesTypes;

    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Security.Cryptography;
    using System.Text;

    using DatabaseContext;

    using LoggingLibrary;

    public class AuthenticateUserGenerateJwt : IAuthenticateUser
    {
        private readonly IAuthenticateUser authenticateUser;

        private readonly ILogger logger;

        private readonly ISecretKeyRetrieval secretKeyRetrieval;

        private readonly IDateTimeService dateTimeService;

        public AuthenticateUserGenerateJwt(IAuthenticateUser authenticateUser, ILogger logger, ISecretKeyRetrieval secretKeyRetrieval, IDateTimeService dateTimeService)
        {
            this.authenticateUser = authenticateUser;
            this.logger = logger;
            this.secretKeyRetrieval = secretKeyRetrieval;
            this.dateTimeService = dateTimeService;
        }

        async Task<AuthenticateUserResponse> IAuthenticateUser.Authenticate(DataContext dataContext, AuthenticateUserRequest authenticateUserRequest)
        {
            var response = await this.authenticateUser.Authenticate(dataContext, authenticateUserRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            try
            {
                this.logger.Info(LogClass.CommRest, "AuthenticateUserGenerateJwt");

                response.JwtToken = generateJwtToken(response.User);

                var refreshToken = new RefreshToken
                {
                    Token = getUniqueToken(),
                    Expires = this.dateTimeService.UtcNow.AddDays(this.secretKeyRetrieval.GetRefreshTokenTTLInDays()),
                    Created = this.dateTimeService.UtcNow,
                    CreatedByIp = authenticateUserRequest.IpAddress,
                    ReasonRevoked = string.Empty,
                    ReplacedByToken = string.Empty
                };

                response.User.CurrentRefreshToken = refreshToken.Token;
                response.User.RefreshToken.Add(refreshToken);
                response.RefreshToken = refreshToken;
            }
            catch (Exception e)
            {
                this.logger.Info(LogClass.CommRest, $"AuthenticateUserGenerateJwt error: {e.Message}");
                response.IsAuthenticated = false;
                response.IsAuthenticated = false;
            }


            return response;

            string getUniqueToken()
            {
                // token is a cryptographically strong random sequence of values
                var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

                // ensure token is unique by checking against db
                var tokenIsUnique = !dataContext.User.Any(u => u.RefreshToken.Any(t => t.Token == token));
                if (!tokenIsUnique)
                {
                    return getUniqueToken();
                }

                return token;
            }

            string generateJwtToken(User user)
            {
                // generate token that is valid for 15 minutes
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(this.secretKeyRetrieval.GetKey());
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] {
                        new Claim("UserId", user.Id.ToString()),
                        new Claim("OrganizationId", user.Organization.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(this.secretKeyRetrieval.GetJwtTokenTTLInMinutes()),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
        }
    }
}
