namespace AuthenticationRepository
{
    using DataPostgresqlLibrary;

    public class UpdateUserUpdate : IUpdateUser
    {
        private readonly IUpdateUser updateUser;

        private readonly ICalculatePassword calculatePassword;

        private readonly ICreatePasswordSalt createPasswordSalt;

        public UpdateUserUpdate(IUpdateUser updateUser, ICalculatePassword calculatePassword, ICreatePasswordSalt createPasswordSalt)
        {
            this.updateUser = updateUser;
            this.calculatePassword = calculatePassword;
            this.createPasswordSalt = createPasswordSalt;
        }

        async Task<UpdateUserResponse> IUpdateUser.Update(DPContext dpContext, UpdateUserRequest updateUserRequest)
        {
            var response = await this.updateUser.Update(dpContext, updateUserRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            response.User.Email = updateUserRequest.UserDto.Email;

            if (response.User.UserName.ToUpper() != AuthenticationConstants.AuthenticationAdminDefaultUserName.ToUpper())
            {
                response.User.UserName = updateUserRequest.UserDto.UserName;
            }

            if (!string.IsNullOrEmpty(updateUserRequest.UserDto.Password) && !string.IsNullOrEmpty(updateUserRequest.UserDto.ConfirmPassword))
            {
                response.User.PasswordSalt = this.createPasswordSalt.CreateSalt(32);
                response.User.Password = this.calculatePassword.calculatePassword(updateUserRequest.UserDto.Password, response.User.PasswordSalt);
            }

            return response;
        }
    }
}
