namespace AuthenticationRepository
{
    using AuthenticationRepositoryTypes;

    using DatabaseContext;

    public class AuthenticateUserCreateAdminIfNecessary : IAuthenticateUser
    {
        private readonly IAuthenticateUser authenticateUser;

        private readonly ICalculatePassword calculatePassword;

        private readonly ICreatePasswordSalt createPasswordSalt;

        public AuthenticateUserCreateAdminIfNecessary(IAuthenticateUser authenticateUser, ICalculatePassword calculatePassword, ICreatePasswordSalt createPasswordSalt)
        {
            this.authenticateUser = authenticateUser;
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

            if (authenticateUserRequest.UserName != AuthenticationConstants.AuthenticationAdminDefaultUserName)
            {
                return response;
            }

            var user = dataContext.User.SingleOrDefault(x => x.UserName == authenticateUserRequest.UserName);
            if (user != null)
            {
                return response;
            }

            var organization = dataContext.Organization.SingleOrDefault(x => x.Name == AuthenticationConstants.AuthenticationAdminOrganizationName);
            if (organization == null)
            {
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

            return response;
        }
    }
}
