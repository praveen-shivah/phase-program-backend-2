namespace AuthenticationRepository
{
    using AuthenticationRepositoryTypes;

    using DatabaseContext;

    public class UpdateUserUpdateLocalInformation : IUpdateUser
    {
        private readonly IUpdateUser updateUser;

        private readonly ICalculatePassword calculatePassword;

        private readonly ICreatePasswordSalt createPasswordSalt;

        public UpdateUserUpdateLocalInformation(IUpdateUser updateUser, ICalculatePassword calculatePassword, ICreatePasswordSalt createPasswordSalt)
        {
            this.updateUser = updateUser;
            this.calculatePassword = calculatePassword;
            this.createPasswordSalt = createPasswordSalt;
        }

        async Task<UpdateUserResponse> IUpdateUser.Update(DataContext dataContext, UpdateUserRequest updateUserRequest)
        {
            var response = await this.updateUser.Update(dataContext, updateUserRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            response.User.Email = updateUserRequest.UpdateUserRequestDto.Email;

            if (response.User.UserName.ToUpper() != AuthenticationConstants.AuthenticationAdminDefaultUserName.ToUpper())
            {
                response.User.UserName = updateUserRequest.UpdateUserRequestDto.UserName;
            }

            return response;
        }
    }
}
