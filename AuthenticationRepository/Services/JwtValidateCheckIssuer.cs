namespace AuthenticationRepository
{
    using AuthenticationRepositoryTypes;

    using Microsoft.IdentityModel.Tokens;

    using SecurityUtilitiesTypes;

    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Text;

    public class JwtValidateCheckIssuer : IJwtValidate
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

                if (jwtToken.Issuer != AuthenticationConstants.REQUIRED_ISSUER)
                {
                    return new JwtValidateResponse() { IsSuccessful = false };
                }

                return new JwtValidateResponse()
                           {
                               IsSuccessful = true,
                               JwtSecurityToken = jwtToken
                           };
            }
            catch (SecurityTokenExpiredException)
            {
                // return null if validation fails
                return new JwtValidateResponse
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
