namespace AuthenticationRepository
{
    using DatabaseContext;

    public interface IUpdateUser
    {
        Task<UpdateUserResponse> Update(DataContext dataContext, UpdateUserRequest updateUserRequest);
    }
}
