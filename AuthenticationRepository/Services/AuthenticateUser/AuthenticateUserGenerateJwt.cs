namespace AuthenticationRepository
{
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Security.Cryptography;
    using System.Text;

    using DataModelsLibrary;

    using DataPostgresqlLibrary;

    using Microsoft.IdentityModel.Tokens;

    using SecurityUtilitiesTypes;

    public class AuthenticateUserGenerateJwt : IAuthenticateUser
    {
        private readonly IAuthenticateUser authenticateUser;

        private readonly ISecretKeyRetrieval secretKeyRetrieval;

        public AuthenticateUserGenerateJwt(IAuthenticateUser authenticateUser, ISecretKeyRetrieval secretKeyRetrieval)
        {
            this.authenticateUser = authenticateUser;
            this.secretKeyRetrieval = secretKeyRetrieval;
        }

        async Task<AuthenticateUserResponse> IAuthenticateUser.Authenticate(DPContext dpContext, AuthenticateUserRequest authenticateUserRequest)
        {
            var response = await this.authenticateUser.Authenticate(dpContext, authenticateUserRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            response.JwtToken = generateJwtToken(response.User);

            var refreshToken = new RefreshToken
            {
                Token = getUniqueToken(),
                Expires = DateTime.UtcNow.AddDays(this.secretKeyRetrieval.GetRefreshTokenTTLInDays()),
                Created = DateTime.UtcNow,
                CreatedByIp = authenticateUserRequest.IpAddress,
                ReasonRevoked = string.Empty,
                ReplacedByToken = string.Empty
            };

            response.User.RefreshTokens.Add(refreshToken);
            response.RefreshToken = refreshToken;

            return response;

            string getUniqueToken()
            {
                // token is a cryptographically strong random sequence of values
                var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

                // ensure token is unique by checking against db
                var tokenIsUnique = !dpContext.User.Any(u => u.RefreshTokens.Any(t => t.Token == token));
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
                    Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                    Expires = DateTime.UtcNow.AddMinutes(this.secretKeyRetrieval.GetJwtTokenTTLInMinutes()),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
        }
    }
}
