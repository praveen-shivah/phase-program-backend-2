namespace ApiHost.Middleware
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Text;

    using Microsoft.IdentityModel.Logging;
    using Microsoft.IdentityModel.Tokens;

    using SecurityUtilitiesTypes;

    public interface IJwtValidate
    {
        public JwtValidateResponse ValidateJwtToken(string? token);
    }

    public class JwtValidate : IJwtValidate
    {
        private readonly ISecretKeyRetrieval secretKeyRetrieval;

        public JwtValidate(ISecretKeyRetrieval secretKeyRetrieval)
        {
            this.secretKeyRetrieval = secretKeyRetrieval;
        }

        JwtValidateResponse IJwtValidate.ValidateJwtToken(string? token)
        {
            if (token == null) return new JwtValidateResponse() { IsSuccessful = false };

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.secretKeyRetrieval.GetKey());
            IdentityModelEventSource.ShowPII = true;
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
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "UserId").Value);
                var organizationId = int.Parse(jwtToken.Claims.First(x => x.Type == "OrganizationId").Value);

                return new JwtValidateResponse
                {
                    IsSuccessful = true,
                    UserId = userId,
                    OrganizationId = organizationId
                };
            }
            catch
            {
                return new JwtValidateResponse() { IsSuccessful = false }; ;
            }
        }
    }
}