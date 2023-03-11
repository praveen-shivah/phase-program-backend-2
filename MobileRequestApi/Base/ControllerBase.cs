namespace ApiHost
{
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    public abstract class ApiControllerBase : Controller
    {
        protected string JwtTokenString
        {
            get
            {
                var token = this.HttpContext.Request.Headers["Access-Token"].FirstOrDefault()?.Split(' ').Last();
                return token ?? string.Empty;
            }
        }

        protected int OrganizationId
        {
            get
            {
                return 1;
                var value = this.HttpContext.Items["OrganizationId"] ?? 0;
                return (int)value;
            }
        }

        protected int UserId
        {
            get
            {
                var value = this.HttpContext.Items["UserId"] ?? 0;
                return (int)value;
            }
        }
    }
}
