namespace AuthenticationRepository;

using DatabaseContext;

using System.IdentityModel.Tokens.Jwt;

public class AuthenticateUserParseClaims : IAuthenticateUser
{
    private IAuthenticateUser authenticateUser;

    public AuthenticateUserParseClaims(IAuthenticateUser authenticateUser)
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

        if (string.IsNullOrEmpty(response.Claims))
        {
            return response;
        }

        // claims for roles:
        // Roles, 5150/2105, Key, Data
        var claimsInfo = response.Claims.Split(',');
        var claims = new Dictionary<string, string>();
        for (var x = 0; x < claimsInfo.Length; x = x + 2)
        {
            claims.Add(claimsInfo[x].ToLower().Trim(), claimsInfo[x + 1].ToLower().Trim());
        }

        var roles = claims["roles"].Split('/');
        foreach (var role in roles)
        {
            if (int.TryParse(role, out var roleAsInt))
            {
                response.Roles.Add(roleAsInt);
            }
        }

        return response;
    }
}