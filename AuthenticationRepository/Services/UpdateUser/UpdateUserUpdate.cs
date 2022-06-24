namespace AuthenticationRepository
{
    using DataPostgresqlLibrary;

    public class UpdateUserUpdate : IUpdateUser
    {
        private readonly IUpdateUser updateUser;

        public UpdateUserUpdate(IUpdateUser updateUser)
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

            response.User.Email = updateUserRequest.UserDto.Email;
            response.User.UserName = updateUserRequest.UserDto.UserName;

            return response;
        }
    }
}
