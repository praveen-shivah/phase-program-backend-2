namespace AuthenticationRepository
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;

    using DataModelsLibrary;

    using Microsoft.IdentityModel.Tokens;

    using SecurityUtilitiesTypes;

    public interface IJwtService
    {
        public string GenerateJwtToken(User user);

        public int? ValidateJwtToken(string? token);

        public Task<RefreshToken> GenerateRefreshToken(User user, string ipAddress);
    }

    public class JwtService : IJwtService
    {
        private readonly IAuthenticationRepository authenticationRepository;

        private readonly ISecretKeyRetrieval secretKeyRetrieval;

        public JwtService(IAuthenticationRepository authenticationRepository, ISecretKeyRetrieval secretKeyRetrieval)
        {
            this.authenticationRepository = authenticationRepository;
            this.secretKeyRetrieval = secretKeyRetrieval;
        }

        string IJwtService.GenerateJwtToken(User user)
        {
            // generate token that is valid for 15 minutes
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.secretKeyRetrieval.GetKey());
            var tokenDescriptor = new SecurityTokenDescriptor
                                      {
                                          Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                                          Expires = DateTime.UtcNow.AddMinutes(15),
                                          SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                                      };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        int? IJwtService.ValidateJwtToken(string? token)
        {
            if (token == null) return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.secretKeyRetrieval.GetKey());
            try
            {
                tokenHandler.ValidateToken(
                    token,
                    new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(key),
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ClockSkew = TimeSpan.Zero
                        },
                    out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                // return user id from JWT token if validation successful
                return userId;
            }
            catch
            {
                // return null if validation fails
                return null;
            }
        }

        async Task<RefreshToken> IJwtService.GenerateRefreshToken(User user, string ipAddress)
        {
            var refreshToken = new RefreshToken
                                   {
                                       Token = await getUniqueToken(),
                                       // token is valid for 7 days
                                       Expires = DateTime.UtcNow.AddDays(7),
                                       Created = DateTime.UtcNow,
                                       CreatedByIp = ipAddress
                                   };

            return refreshToken;

            async Task<string> getUniqueToken()
            {
                // token is a cryptographically strong random sequence of values
                var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
                
                // ensure token is unique by checking against db
                var response = await this.authenticationRepository.RefreshToken(token, user.Id, ipAddress);

                if (response.IsSuccessful)
                {
                    return token;
                }

                return token;
            }
        }
    }
}