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

            var user = await dpContext.User.SingleOrDefaultAsync(x => x.Id == updateUserRequest.UserDto.Id);
            response.User = user ?? new User();

            return response;
        }
    }
}
