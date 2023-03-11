namespace ApiHost.Middleware
{
    using AuthenticationRepository;

    using AuthenticationRepositoryTypes;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;

    using SecurityUtilitiesTypes;

    using System.Linq;
    using System.Threading.Tasks;

    public static class ValidateAPICallMiddleWareExtensions
    {
        public static IApplicationBuilder UseValidateAPICall(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ValidateAPICallMiddleWare>();
        }
    }

    public class ValidateAPICallMiddleWare
    {
        private readonly RequestDelegate next;

        private readonly IJwtValidate jwtValidate;

        private readonly ISecretKeyRetrieval secretKeyRetrieval;

        public ValidateAPICallMiddleWare(RequestDelegate next,
                                         IJwtValidate jwtValidate,
                                         ISecretKeyRetrieval secretKeyRetrieval)
        {
            this.next = next;
            this.jwtValidate = jwtValidate;
            this.secretKeyRetrieval = secretKeyRetrieval;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                // Validate token  If not valid let through, but user won't be set.
                var token = context.Request.Headers["Access-Token"].FirstOrDefault()?.Split(' ').Last();
                if (!string.IsNullOrEmpty(token))
                {
                    JwtValidateResponse jwtValidateResponse = this.jwtValidate.ValidateJwtToken(token, this.secretKeyRetrieval);
                    if (jwtValidateResponse.IsSuccessful)
                    {
                        if (jwtValidateResponse.IsExpired)
                        {
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            return;
                        }
                        else
                        {
                            context.Items["JwtSecurityToken"] = jwtValidateResponse.JwtSecurityToken;
                        }
                    }
                    else
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        return;
                    }
                }

                await this.next(context);
            }
            catch
            {
                await this.next(context);
            }
        }
    }
}