namespace ApiHost.Middleware
{
    using AuthenticationRepository;

    using AuthenticationRepositoryTypes;

    using Microsoft.IdentityModel.Tokens;

    using SecurityUtilitiesTypes;

    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Text;

    public class JwtValidateDefault : IJwtValidate
    {
        JwtValidateResponse IJwtValidate.ValidateJwtToken(string token, ISecretKeyRetrieval secretKeyRetrieval)
        {
            if (token == null)
            {
                return new JwtValidateResponse
                {
                    IsSuccessful = false
                };
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKeyRetrieval.GetKey());
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
                    out var validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                return new JwtValidateResponse()
                {
                    IsSuccessful = true,
                    JwtSecurityToken = jwtToken
                };
            }
            catch (SecurityTokenExpiredException)
            {
                return new JwtValidateResponse()
                {
                    IsSuccessful = true,
                    IsExpired = true
                };
            }
            catch
            {
                // return null if validation fails
                return new JwtValidateResponse
                {
                    IsSuccessful = false
                };
            }
        }
    }
}
