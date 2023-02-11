namespace AuthenticationRepository
{
    using AuthenticationRepositoryTypes;

    using DatabaseContext;
    using LoggingLibrary;

    public class AuthenticateUserCreateAdminIfNecessary : IAuthenticateUser
    {
        private readonly IAuthenticateUser authenticateUser;

        private readonly ILogger logger;

        private readonly ICalculatePassword calculatePassword;

        private readonly ICreatePasswordSalt createPasswordSalt;

        public AuthenticateUserCreateAdminIfNecessary(IAuthenticateUser authenticateUser, ILogger logger, ICalculatePassword calculatePassword, ICreatePasswordSalt createPasswordSalt)
        {
            this.authenticateUser = authenticateUser;
            this.logger = logger;
            this.calculatePassword = calculatePassword;
            this.createPasswordSalt = createPasswordSalt;
        }

        async Task<AuthenticateUserResponse> IAuthenticateUser.Authenticate(DataContext dataContext, AuthenticateUserRequest authenticateUserRequest)
        {
            var response = await this.authenticateUser.Authenticate(dataContext, authenticateUserRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            this.logger.Info(LogClass.CommRest, "AuthenticateUserCreateAdminIfNecessary");

            if (authenticateUserRequest.UserName != AuthenticationConstants.AuthenticationAdminDefaultUserName)
            {
                return response;
            }

            var user = dataContext.User.SingleOrDefault(x => x.UserName == authenticateUserRequest.UserName);
            if (user != null)
            {
                this.logger.Info(LogClass.CommRest, "AuthenticateUserCreateAdminIfNecessary found admin user");
                return response;
            }

            this.logger.Info(LogClass.CommRest, "AuthenticateUserCreateAdminIfNecessary did not find admin user");
            var organization = dataContext.Organization.SingleOrDefault(x => x.Name == AuthenticationConstants.AuthenticationAdminOrganizationName);
            if (organization == null)
            {
                this.logger.Info(LogClass.CommRest, "AuthenticateUserCreateAdminIfNecessary did not find admin organization");

                organization = new Organization()
                                   {
                                       Apikey = string.Empty,
                                       Name = AuthenticationConstants.AuthenticationAdminOrganizationName,
                                       Password = string.Empty,
                                       Url = string.Empty,
                                       UserId = string.Empty
                                   };
                dataContext.Organization.Add(organization);
                await dataContext.SaveChangesAsync();
            }

            this.logger.Info(LogClass.CommRest, "AuthenticateUserCreateAdminIfNecessary creating admin user");
            var salt = this.createPasswordSalt.CreateSalt(32);
            user = new User()
                       {
                           Email = AuthenticationConstants.AuthenticationAdminDefaultEmail,
                           UserName = AuthenticationConstants.AuthenticationAdminDefaultUserName,
                           IsActive = true,
                           PasswordSalt = salt,
                           Organization = organization,
                           Password = this.calculatePassword.calculatePassword(AuthenticationConstants.AuthenticationAdminDefaultPassword, salt),
                           CurrentRefreshToken = string.Empty
                       };
            dataContext.User.Add(user);
            await dataContext.SaveChangesAsync();

            this.logger.Info(LogClass.CommRest, "AuthenticateUserCreateAdminIfNecessary admin user created");

            return response;
        }
    }
}
