namespace AuthenticationRepository
{
    using DataPostgresqlLibrary;

    public interface IUpdateUser
    {
        Task<UpdateUserResponse> Update(DPContext dpContext, UpdateUserRequest updateUserRequest);
    }
}
