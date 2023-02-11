namespace AuthenticationRepository
{
    using ApiDTO;

    using CommonServices;

    using SecurityUtilitiesTypes;

    using System.Security.Cryptography;

    using DatabaseContext;

    public class RefreshTokenRetrieveRefreshToken : IRefreshToken
    {
        private readonly IDateTimeService dateTimeService;

        private readonly IJwtService jwtService;

        private readonly IRefreshToken refreshToken;

        private readonly ISecretKeyRetrieval secretKeyRetrieval;
        private readonly IJwtValidate jwtValidate;

        public RefreshTokenRetrieveRefreshToken(
            IRefreshToken refreshToken,
            IDateTimeService dateTimeService,
            IJwtService jwtService,
            ISecretKeyRetrieval secretKeyRetrieval,
            IJwtValidate jwtValidate)
        {
            this.refreshToken = refreshToken;
            this.dateTimeService = dateTimeService;
            this.jwtService = jwtService;
            this.secretKeyRetrieval = secretKeyRetrieval;
            this.jwtValidate = jwtValidate;
        }

        async Task<RefreshTokenResponse> IRefreshToken.Refresh(DataContext dataContext, RefreshTokenRequest refreshTokenRequest)
        {
            var response = await this.refreshToken.Refresh(dataContext, refreshTokenRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            try
            {
                var refreshToken = response.User.RefreshToken.Single(x => x.Token == refreshTokenRequest.RefreshToken);
                if (refreshToken.IsRevoked)
                {
                    this.revokeDescendantRefreshTokens(refreshToken, response.User, refreshTokenRequest.IpAddress, $"Attempted reuse of revoked ancestor token: {refreshToken.Token}");
                    response.IsSuccessful = false;
                    response.RefreshTokenResponseType = RefreshTokenResponseType.attemptedReuse;
                    return response;
                }

                if (!refreshToken.IsActive)
                {
                    response.IsSuccessful = false;
                    response.RefreshTokenResponseType = RefreshTokenResponseType.notActive;
                    return response;
                }

                var newRefreshToken = this.rotateRefreshToken(dataContext, refreshTokenRequest.IpAddress);
                response.User.CurrentRefreshToken = newRefreshToken.Token;
                response.User.RefreshToken.Add(newRefreshToken);
                response.RefreshToken = new RefreshTokenDto
                {
                    Token = newRefreshToken.Token,
                    Created = newRefreshToken.Created,
                    CreatedByIp = newRefreshToken.CreatedByIp,
                    Expires = newRefreshToken.Expires
                };

                this.removeOldRefreshTokens(response.User);

                response.JwtToken = this.jwtService.GenerateJwtToken(response.User);

                var validateResponse = this.jwtValidate.ValidateJwtToken(response.JwtToken);
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.RefreshTokenResponseType = RefreshTokenResponseType.duplicated;
            }

            return response;
        }

        private void removeOldRefreshTokens(User user)
        {
            // remove old inactive refresh tokens from user based on TTL in app settings
            var list = user.RefreshToken.ToList();
            list.RemoveAll(x => !x.IsActive && x.Created.AddDays(this.secretKeyRetrieval.GetRefreshTokenTTLInDays()) <= DateTime.UtcNow);
        }

        private void revokeDescendantRefreshTokens(
            RefreshToken refreshToken,
            User user,
            string ipAddress,
            string reason)
        {
            // recursively traverse the refresh token chain and ensure all descendants are revoked
            if (string.IsNullOrEmpty(refreshToken.ReplacedByToken))
            {
                return;
            }

            var childToken = user.RefreshToken.SingleOrDefault(x => x.Token == refreshToken.ReplacedByToken);

            switch (childToken)
            {
                case null:
                    return;
                case { IsActive: true }:
                    this.revokeRefreshToken(childToken, ipAddress, reason);
                    break;
                default:
                    this.revokeDescendantRefreshTokens(childToken, user, ipAddress, reason);
                    break;
            }
        }

        private void revokeRefreshToken(
            RefreshToken token,
            string ipAddress,
            string reason = null,
            string replacedByToken = null)
        {
            token.Revoked = DateTime.UtcNow;
            token.RevokedByIp = ipAddress;
            token.ReasonRevoked = reason;
            token.ReplacedByToken = replacedByToken;
        }

        private RefreshToken rotateRefreshToken(DataContext dataContext, string ipAddress)
        {
            var refreshToken = new RefreshToken
            {
                Token = getUniqueToken(),
                Expires = this.dateTimeService.UtcNow.AddDays(this.secretKeyRetrieval.GetRefreshTokenTTLInDays()),
                Created = this.dateTimeService.UtcNow,
                CreatedByIp = ipAddress,
                ReasonRevoked = string.Empty,
                ReplacedByToken = string.Empty
            };

            return refreshToken;

            string getUniqueToken()
            {
                while (true)
                {
                    // token is a cryptographically strong random sequence of values
                    var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

                    // ensure token is unique by checking against db
                    var tokenIsUnique = !dataContext.User.Any(u => u.RefreshToken.Any(t => t.Token == token));
                    if (!tokenIsUnique)
                    {
                        continue;
                    }

                    return token;
                }
            }
        }
    }
}