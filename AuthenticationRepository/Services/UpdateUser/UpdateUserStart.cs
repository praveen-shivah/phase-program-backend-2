namespace AuthenticationRepository
{
    using System.Net;

    using DatabaseContext;

    using RestServicesSupportTypes;

    public class UpdateUserStart : IUpdateUser
    {
        Task<UpdateUserResponse> IUpdateUser.Update(DataContext dataContext, UpdateUserRequest updateUserRequest)
        {
            return Task.FromResult(new UpdateUserResponse
            {
                IsSuccessful = true,
                ErrorMessage = string.Empty,
                HttpStatusCode = HttpStatusCode.OK,
                ResponseTypeEnum = ResponseTypeEnum.success
            });
        }
    }
}
