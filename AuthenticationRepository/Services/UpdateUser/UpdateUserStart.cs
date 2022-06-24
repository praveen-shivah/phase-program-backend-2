namespace AuthenticationRepository
{
    using DataPostgresqlLibrary;

    public class UpdateUserStart : IUpdateUser
    {
        Task<UpdateUserResponse> IUpdateUser.Update(DPContext dpContext, UpdateUserRequest updateUserRequest)
        {
            return Task.FromResult(new UpdateUserResponse { IsSuccessful = true });
        }
    }
}
