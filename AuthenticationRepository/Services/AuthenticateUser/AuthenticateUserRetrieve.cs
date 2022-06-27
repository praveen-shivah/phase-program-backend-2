namespace AuthenticationRepository
{

    using DataPostgresqlLibrary;

    using Microsoft.EntityFrameworkCore;

    public class AuthenticateUserRetrieve : IAuthenticateUser
    {
        private readonly IAuthenticateUser authenticateUser;

        private readonly ICalculatePassword calculatePassword;

        public AuthenticateUserRetrieve(IAuthenticateUser authenticateUser, ICalculatePassword calculatePassword)
        {
            this.authenticateUser = authenticateUser;
            this.calculatePassword = calculatePassword;
        }

        async Task<AuthenticateUserResponse> IAuthenticateUser.Authenticate(DPContext dpContext, AuthenticateUserRequest authenticateUserRequest)
        {
            var response = await this.authenticateUser.Authenticate(dpContext, authenticateUserRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            var user = dpContext.User.Include(r => r.RefreshTokens).Include(o=>o.Organization).SingleOrDefault(x => x.UserName == authenticateUserRequest.UserName && x.IsActive);
            if (user == null || user.Password != this.calculatePassword.calculatePassword(authenticateUserRequest.Password, user.PasswordSalt))
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
    }
}
