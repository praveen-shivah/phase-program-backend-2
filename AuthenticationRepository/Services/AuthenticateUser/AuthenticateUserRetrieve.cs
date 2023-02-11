namespace AuthenticationRepository
{
    using DatabaseContext;

    using LoggingLibrary;

    using Microsoft.EntityFrameworkCore;

    public class AuthenticateUserRetrieve : IAuthenticateUser
    {
        private readonly IAuthenticateUser authenticateUser;

        private readonly ILogger logger;

        private readonly ICalculatePassword calculatePassword;

        public AuthenticateUserRetrieve(IAuthenticateUser authenticateUser, ILogger logger, ICalculatePassword calculatePassword)
        {
            this.authenticateUser = authenticateUser;
            this.logger = logger;
            this.calculatePassword = calculatePassword;
        }

        async Task<AuthenticateUserResponse> IAuthenticateUser.Authenticate(DataContext dataContext, AuthenticateUserRequest authenticateUserRequest)
        {
            var response = await this.authenticateUser.Authenticate(dataContext, authenticateUserRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            this.logger.Info(LogClass.CommRest, "AuthenticateUserRetrieve");

            var user = dataContext.User.Include(r => r.RefreshToken).Include(o=>o.Organization).SingleOrDefault(x => x.UserName == authenticateUserRequest.UserName && x.IsActive);
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
