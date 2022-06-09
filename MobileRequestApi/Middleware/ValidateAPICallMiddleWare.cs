namespace ApiHost.Middleware
{
    using System.Linq;
    using System.Threading.Tasks;

    using AuthenticationRepository;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;

    using OrganizationRepositoryTypes;

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

        private readonly IOrganizationRepository organizationRepository;

        private readonly IAuthenticationRepository authenticationRepository;

        public ValidateAPICallMiddleWare(RequestDelegate next, ILoggerFactory loggerFactory, IJwtValidate jwtValidate, IOrganizationRepository organizationRepository, IAuthenticationRepository authenticationRepository)
        {
            this.next = next;
            this.jwtValidate = jwtValidate;
            this.organizationRepository = organizationRepository;
            this.authenticationRepository = authenticationRepository;
        }

        public async Task Invoke(HttpContext context)
        {
            // Validate token  If not valid let through, but user won't be set.
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = this.jwtValidate.ValidateJwtToken(token);
            if (userId != null)
            {
                // attach user to context on successful jwt validation
                var response = await this.authenticationRepository.GetUserById(userId.Value);
                if (!response.IsAuthenticated || !response.IsSuccessful)
                {
                    context.Items["UserId"] = null;
                    await this.next(context);
                    return;
                }
            }

            var headers = context.Request.Headers;
            var org = headers["OrganizationId"];
            if (!context.Request.Headers.TryGetValue("OrganizationId", out var organizationId))
            {
                context.Items["UserId"] = null;
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized client. OrganizationId missing");
                return;
            }

            if (!context.Request.Headers.TryGetValue("APIKey", out var apiKey))
            {
                context.Items["UserId"] = null;
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized client. APIKey missing");
                return;
            }

            var organizationResponse = await this.organizationRepository.GetOrganizationRequestAsync(
                new OrganizationRequest()
                    {
                        OrganizationId = organizationId,
                        APIKey = apiKey
                    });

            if (!organizationResponse.IsSuccessful)
            {
                context.Items["UserId"] = null;
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized client. Invalid data.");
                return;
            }

            context.Items["UserId"] = userId;

            await this.next(context);
        }
    }
}