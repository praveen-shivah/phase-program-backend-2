namespace ApiHost.Middleware
{
    using System.Linq;
    using System.Threading.Tasks;

    using AuthenticationRepository;

    using LoggingLibrary;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;

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

        private readonly ILogger logger;

        private readonly IJwtValidate jwtValidate;

        private readonly IOrganizationRepository organizationRepository;

        private readonly IAuthenticationRepository authenticationRepository;

        public ValidateAPICallMiddleWare(RequestDelegate next, ILogger logger, IJwtValidate jwtValidate, IOrganizationRepository organizationRepository, IAuthenticationRepository authenticationRepository)
        {
            this.next = next;
            this.logger = logger;
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
                    this.logger.Info(LogClass.CommRest, "ValidateAPICallMiddleWare");
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

                this.logger.Info(LogClass.CommRest, "ValidateAPICallMiddleWare 2");
                await this.next(context);
            }
            catch
            {
                context.Items["OrganizationId"] = null;
                context.Items["UserId"] = null;
                this.logger.Info(LogClass.CommRest, "ValidateAPICallMiddleWare 3");
                await this.next(context);
            }
        }
    }
}