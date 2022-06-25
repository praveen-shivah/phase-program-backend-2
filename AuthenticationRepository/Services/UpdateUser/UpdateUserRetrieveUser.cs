namespace AuthenticationRepository
{
    using DataModelsLibrary;

    using DataPostgresqlLibrary;

    using Microsoft.EntityFrameworkCore;

    public class UpdateUserRetrieveUser : IUpdateUser
    {
        private readonly IUpdateUser updateUser;

        public UpdateUserRetrieveUser(IUpdateUser updateUser)
        {
            this.updateUser = updateUser;
        }

        async Task<UpdateUserResponse> IUpdateUser.Update(DPContext dpContext, UpdateUserRequest updateUserRequest)
        {
            var response = await this.updateUser.Update(dpContext, updateUserRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            var user = await dpContext.User.Include(o => o.Organization).SingleOrDefaultAsync(x => x.Id == updateUserRequest.UserDto.Id);
            if (user == null)
            {
                var organization = await dpContext.Organization.SingleAsync(o => o.Id == updateUserRequest.OrganizationId);
                user = new User { Organization = organization, CurrentRefreshToken = string.Empty};
                dpContext.User.Add(user);
            }

            response.User = user;

            return response;
        }
    }
}
