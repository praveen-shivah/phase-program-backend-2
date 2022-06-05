namespace AuthenticationRepository
{
    using System.Security.Cryptography;
    using System.Text;

    using DataModelsLibrary;

    using DataPostgresqlLibrary;

    using Microsoft.EntityFrameworkCore;

    using SharedUtilities;

    public class AuthenticateUserCreateAdminIfNecessary : IAuthenticateUser
    {
        private readonly IAuthenticateUser authenticateUser;

        public AuthenticateUserCreateAdminIfNecessary(IAuthenticateUser authenticateUser)
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

            var salt = this.createSalt(32);
            user = new User()
                       {
                           Email = AuthenticationConstants.AuthenticationAdminDefaultEmail,
                           UserName = AuthenticationConstants.AuthenticationAdminDefaultUserName,
                           PasswordSalt = salt,
                           Organization = organization,
                           Password = this.calculatePasswordHash(AuthenticationConstants.AuthenticationAdminDefaultPassword, salt)
                       };
            dpContext.User.Add(user);
            await dpContext.SaveChangesAsync();

            return response;
        }

        private string createSalt(int size)
        {
            using (var generator = RandomNumberGenerator.Create())
            {
                var salt = new byte[size];
                generator.GetBytes(salt);
                return Convert.ToBase64String(salt); ;
            }
        }

        private string calculatePasswordHash(string password, string passwordSalt)
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
