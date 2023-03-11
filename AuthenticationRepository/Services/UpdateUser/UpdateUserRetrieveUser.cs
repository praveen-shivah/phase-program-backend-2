namespace AuthenticationRepository
{
    using System.Net;

    using AuthenticationRepositoryTypes;

    using DatabaseContext;

    using Microsoft.EntityFrameworkCore;

    using RestServicesSupportTypes;

    public class UpdateUserRetrieveUser : IUpdateUser
    {
        private readonly IUpdateUser updateUser;

        public UpdateUserRetrieveUser(IUpdateUser updateUser)
        {
            this.updateUser = updateUser;
        }

        async Task<UpdateUserResponse> IUpdateUser.Update(DataContext dataContext, UpdateUserRequest updateUserRequest)
        {
            var response = await this.updateUser.Update(dataContext, updateUserRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            var user = await dataContext.User.Include(o => o.Organization).SingleOrDefaultAsync(x => x.Id == updateUserRequest.UpdateUserRequestDto.Id);
            if (user == null)
            {
                if (updateUserRequest.UpdateUserRequestDto.UserName.ToUpper() == AuthenticationConstants.AuthenticationAdminDefaultUserName.ToUpper())
                {
                    response.IsSuccessful = false;
                    response.ResponseTypeEnum = ResponseTypeEnum.idNotFound;
                    response.ErrorMessage = $"Username {updateUserRequest.UpdateUserRequestDto.UserName} not found";
                    response.HttpStatusCode = HttpStatusCode.BadGateway;

                    return response;
                }

                var organization = await dataContext.Organization.SingleAsync(o => o.Id == updateUserRequest.OrganizationId);
                user = new User
                {
                    Organization = organization,
                    CurrentRefreshToken = string.Empty,
                    UserName = updateUserRequest.UpdateUserRequestDto.UserName,
                    Email = updateUserRequest.UpdateUserRequestDto.Email,
                    IsActive = updateUserRequest.UpdateUserRequestDto.IsActive
                };
                dataContext.User.Add(user);
            }

            response.User = user;

            return response;
        }
    }
}