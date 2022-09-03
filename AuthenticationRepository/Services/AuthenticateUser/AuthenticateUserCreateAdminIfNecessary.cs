namespace AuthenticationRepository
{
    using AuthenticationRepositoryTypes;

    using DataModelsLibrary;

    using DataPostgresqlLibrary;

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

        async Task<AuthenticateUserResponse> IAuthenticateUser.Authenticate(DPContext dpContext, AuthenticateUserRequest authenticateUserRequest)
        {
            var response = await this.authenticateUser.Authenticate(dpContext, authenticateUserRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            if (authenticateUserRequest.UserName != AuthenticationConstants.AuthenticationAdminDefaultUserName)
            {
                return response;
            }

            var user = dpContext.User.SingleOrDefault(x => x.UserName == authenticateUserRequest.UserName);
            if (user != null)
            {
                return response;
            }

            var organization = dpContext.Organization.SingleOrDefault(x => x.Name == AuthenticationConstants.AuthenticationAdminOrganizationName);
            if (organization == null)
            {
                organization = new Organization()
                                   {
                                       APIKey = string.Empty,
                                       Name = AuthenticationConstants.AuthenticationAdminOrganizationName,
                                       Password = string.Empty,
                                       URL = string.Empty,
                                       UserId = string.Empty
                                   };
                dpContext.Organization.Add(organization);
                await dpContext.SaveChangesAsync();
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
            dpContext.User.Add(user);
            await dpContext.SaveChangesAsync();

            return response;
        }
    }
}
