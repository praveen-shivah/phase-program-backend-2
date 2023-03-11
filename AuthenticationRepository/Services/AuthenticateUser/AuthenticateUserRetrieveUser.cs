namespace AuthenticationRepository;

using System.Net;

using DatabaseContext;

using Microsoft.EntityFrameworkCore;

using RestServicesSupportTypes;

public class AuthenticateUserRetrieveUser : IAuthenticateUser
{
    private IAuthenticateUser authenticateUser;

    public AuthenticateUserRetrieveUser(IAuthenticateUser authenticateUser)
    {
        this.authenticateUser = authenticateUser;
    }

    async Task<AuthenticateUserResponse> IAuthenticateUser.AuthenticateUserAsync(DataContext context, AuthenticateUserRequest request)
    {
        var response = await this.authenticateUser.AuthenticateUserAsync(context, request);
        if (!response.IsSuccessful)
        {
            return response;
        }

        var user = await context.User.Include(o => o.Organization).SingleOrDefaultAsync(x => x.UserName.ToLower().Trim() == request.UserName.ToLower().Trim());
        if (user == null)
        {
            response.IsAuthenticated = false;
            response.IsSuccessful = false;
            response.ResponseTypeEnum = ResponseTypeEnum.idNotFound;
            response.HttpStatusCode = HttpStatusCode.BadRequest;
            response.ErrorMessage = $"Username {request.UserName} not found";

            return response;
        }

        if (user.IsActive)
        {
            return response;
        }

        response.IsAuthenticated = false;
        response.IsSuccessful = false;
        response.ResponseTypeEnum = ResponseTypeEnum.disabled;
        response.HttpStatusCode = HttpStatusCode.BadRequest;
        response.ErrorMessage = $"Username {request.UserName} not active";

        return response;
    }
}