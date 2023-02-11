namespace AuthenticationRepository
{
    using AuthenticationRepositoryTypes;

    using DatabaseContext;

    using Microsoft.EntityFrameworkCore;

    public class UpdateUserRetrieveUser : IUpdateUser
    {
        private readonly IUpdateUser updateUser;

        public UpdateUserRetrieveUser(IUpdateUser updateUser)
        {
            this.updateUser = updateUser;
        }

        async Task<UpdateUserResponse> IUpdateUser.Update(DataContext dataContext, UpdateUserRequest updateUserRequest)
        {
            var response = await this.updateUser.Update(dataContext, updateUserRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            var user = await dataContext.User.Include(o => o.Organization).SingleOrDefaultAsync(x => x.Id == updateUserRequest.UserDto.Id);
            if (user == null)
            {
                if (updateUserRequest.UserDto.UserName.ToUpper() == AuthenticationConstants.AuthenticationAdminDefaultUserName.ToUpper())
                {
                    response.IsSuccessful = false;
                    return response;
                }

                var organization = await dataContext.Organization.SingleAsync(o => o.Id == updateUserRequest.OrganizationId);
                user = new User
                           {
                               Organization = organization,
                               CurrentRefreshToken = string.Empty,
                               UserName = updateUserRequest.UserDto.UserName,
                               Email = updateUserRequest.UserDto.Email
                           };
                dataContext.User.Add(user);
            }

            response.User = user;

            return response;
        }
    }
}