namespace AuthenticationRepository
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;

    using CommonServices;

    using DataModelsLibrary;

    using Microsoft.IdentityModel.Tokens;

    using SecurityUtilitiesTypes;

    public interface IJwtService
    {
        public string GenerateJwtToken(User user);

        public int? ValidateJwtToken(string? token);
    }

    public class JwtService : IJwtService
    {
        private readonly ISecretKeyRetrieval secretKeyRetrieval;

        private readonly IDateTimeService dateTimeService;

        public JwtService(ISecretKeyRetrieval secretKeyRetrieval, IDateTimeService dateTimeService)
        {
            this.secretKeyRetrieval = secretKeyRetrieval;
            this.dateTimeService = dateTimeService;
        }

        string IJwtService.GenerateJwtToken(User user)
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
    }
}