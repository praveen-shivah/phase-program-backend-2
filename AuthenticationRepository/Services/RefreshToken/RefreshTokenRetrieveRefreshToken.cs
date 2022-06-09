namespace AuthenticationRepository
{
    using AuthenticationRepositoryTypes;

    using CommonServices;

    using DataModelsLibrary;

    using DataPostgresqlLibrary;

    using SecurityUtilitiesTypes;

    public class RefreshTokenRetrieveRefreshToken : IRefreshToken
    {
        private readonly IDateTimeService dateTimeService;

        private readonly IJwtService jwtService;

        private readonly IRefreshToken refreshToken;

        private readonly ISecretKeyRetrieval secretKeyRetrieval;

        public RefreshTokenRetrieveRefreshToken(
            IRefreshToken refreshToken,
            IDateTimeService dateTimeService,
            IJwtService jwtService,
            ISecretKeyRetrieval secretKeyRetrieval)
        {
            this.refreshToken = refreshToken;
            this.dateTimeService = dateTimeService;
            this.jwtService = jwtService;
            this.secretKeyRetrieval = secretKeyRetrieval;
        }

        async Task<RefreshTokenResponse> IRefreshToken.Refresh(DPContext dpContext, RefreshTokenRequest refreshTokenRequest)
        {
            var response = await this.refreshToken.Refresh(dpContext, refreshTokenRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            try
            {
                var refreshToken = response.User.RefreshTokens.Single(x => x.Token == refreshTokenRequest.RefreshToken);
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

                var newRefreshToken = await this.rotateRefreshToken(refreshToken, response.User, refreshTokenRequest.IpAddress);
                response.User.RefreshTokens.Add(newRefreshToken);

                this.removeOldRefreshTokens(response.User);

                response.JwtToken = this.jwtService.GenerateJwtToken(response.User);
            }
            catch
            {
                response.IsSuccessful = false;
                response.RefreshTokenResponseType = RefreshTokenResponseType.duplicated;
            }

            return response;
        }

        private void removeOldRefreshTokens(User user)
        {
            // remove old inactive refresh tokens from user based on TTL in app settings
            user.RefreshTokens.RemoveAll(x => !x.IsActive && x.Created.AddDays(this.secretKeyRetrieval.GetRefreshTokenTTL()) <= DateTime.UtcNow);
        }

        private void revokeDescendantRefreshTokens(
            RefreshToken refreshToken,
            User user,
            string ipAddress,
            string reason)
        {
            // recursively traverse the refresh token chain and ensure all descendants are revoked
            if (!string.IsNullOrEmpty(refreshToken.ReplacedByToken))
            {
                var childToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken.ReplacedByToken);
                if (childToken != null && childToken.IsActive)
                {
                    this.revokeRefreshToken(childToken, ipAddress, reason);
                }
                else
                {
                    this.revokeDescendantRefreshTokens(childToken, user, ipAddress, reason);
                }
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

        private async Task<RefreshToken> rotateRefreshToken(RefreshToken refreshToken, User user, string ipAddress)
        {
            var newRefreshToken = await this.jwtService.GenerateRefreshToken(user, ipAddress);
            this.revokeRefreshToken(refreshToken, ipAddress, "Replaced by new token", newRefreshToken.Token);
            return newRefreshToken;
        }
    }
}