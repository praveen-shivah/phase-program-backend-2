namespace ApiHost.Middleware
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Text;

    using Microsoft.IdentityModel.Tokens;

    using SecurityUtilitiesTypes;

    public interface IJwtValidate
    {
        public int? ValidateJwtToken(string? token);
    }

    public class JwtValidate : IJwtValidate
    {
        private readonly ISecretKeyRetrieval secretKeyRetrieval;

        public JwtValidate(ISecretKeyRetrieval secretKeyRetrieval)
        {
            this.secretKeyRetrieval = secretKeyRetrieval;
        }

        int? IJwtValidate.ValidateJwtToken(string? token)
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

                return userId;
            }
            catch
            {
                return null;
            }
        }
    }
}