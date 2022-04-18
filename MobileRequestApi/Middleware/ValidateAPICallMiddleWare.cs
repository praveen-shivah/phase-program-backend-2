namespace MobileRequestApi.Middleware
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
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
        private readonly ILogger logger;

        private readonly RequestDelegate next;

        private readonly IOrganizationRepository organizationRepository;

        public ValidateAPICallMiddleWare(RequestDelegate next, ILoggerFactory loggerFactory, IOrganizationRepository organizationRepository)
        {
            this.next = next;
            this.organizationRepository = organizationRepository;
            this.logger = loggerFactory.CreateLogger<RequestResponseLoggingMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue("OrganizationId", out var organizationId))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized client. OrganizationId missing");
                return;
            }

            if (!context.Request.Headers.TryGetValue("APIKey", out var apiKey))
            {
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
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized client. Invalid data.");
                return;
            }

            await this.next(context);
        }
    }
}