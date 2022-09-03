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
            try
            {
                // Validate token  If not valid let through, but user won't be set.
                var token = context.Request.Headers["Access-Token"].FirstOrDefault()?.Split(" ").Last();
                var jwtValidateResponse = this.jwtValidate.ValidateJwtToken(token);
                if (jwtValidateResponse.IsSuccessful)
                {
                    // attach user to context on successful jwt validation
                    var response = await this.authenticationRepository.GetUserById(jwtValidateResponse.UserId);
                    if (!response.IsAuthenticated || !response.IsSuccessful)
                    {
                        context.Items["UserId"] = null;
                        await this.next(context);
                        return;
                    }

                    context.Items["UserId"] = jwtValidateResponse.UserId;
                    context.Items["OrganizationId"] = jwtValidateResponse.OrganizationId;
                } else
                {
                    // Anonymous or not - must have valid organizationId/key
                    var organizationId = 0;
                    var values = context.Request.Headers["OrganizationId"];
                    context.Items["UserId"] = null;
                    context.Items["OrganizationId"] = null;

                    if (values.Count > 0)
                    {
                        organizationId = int.Parse(values[0]);
                    }

                    if (context.Request.Headers.TryGetValue("APIKey", out var apiKey))
                    {
                        var organizationResponse = await this.organizationRepository.GetOrganizationRequestAsync(
                                                       new OrganizationRequest()
                                                       {
                                                           OrganizationId = organizationId.ToString(),
                                                           APIKey = apiKey
                                                       });

                        if (organizationResponse.IsSuccessful)
                        {
                            context.Items["OrganizationId"] = organizationId;
                        }
                    }
                }

                await this.next(context);
            }
            catch
            {
                context.Items["OrganizationId"] = null;
                context.Items["UserId"] = null;
                await this.next(context);
            }
        }
    }
}