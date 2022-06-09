namespace AuthenticationRepository
{
    using System.Security.Cryptography;
    using System.Text;

    using DataPostgresqlLibrary;

    using Microsoft.EntityFrameworkCore;

    using SharedUtilities;

    public class AuthenticateUserRetrieve : IAuthenticateUser
    {
        private readonly IAuthenticateUser authenticateUser;

        public AuthenticateUserRetrieve(IAuthenticateUser authenticateUser)
        {
            this.authenticateUser = authenticateUser;
        }

        async Task<AuthenticateUserResponse> IAuthenticateUser.Authenticate(DPContext dpContext, AuthenticateUserRequest authenticateUserRequest)
        {
            var response = await this.authenticateUser.Authenticate(dpContext, authenticateUserRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            var user = dpContext.User.Include(r => r.RefreshTokens).SingleOrDefault(x => x.UserName == authenticateUserRequest.UserName);
            if (user == null || user.Password != this.calculatePassword(authenticateUserRequest.Password, user.PasswordSalt))
            {
                response.IsSuccessful = false;
                response.IsAuthenticated = false;
                return response;
            }

            var temporaryRolesList = new List<int>
                                         {
                                             2001,
                                             5150
                                         };

            response.User = user;
            response.IsAuthenticated = true;
            response.UserId = user.Id;
            response.UserName = user.UserName;
            response.Roles = temporaryRolesList;

            return response;
        }

        private string calculatePassword(string password, string passwordSalt)
        {
            using (var sha256 = SHA256.Create())
            {
                var combined = $"{password}{passwordSalt}";
                var hash = sha256.ComputeHash(Encoding.Unicode.GetBytes(combined));
                return hash.ByteArrayToHexString();
            }
        }
    }
}
