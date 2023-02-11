namespace AuthenticationRepository
{
    using DatabaseContext;

    public class UpdateUserStart : IUpdateUser
    {
        Task<UpdateUserResponse> IUpdateUser.Update(DataContext dataContext, UpdateUserRequest updateUserRequest)
        {
            return Task.FromResult(new UpdateUserResponse { IsSuccessful = true });
        }
    }
}
