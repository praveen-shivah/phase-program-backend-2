namespace ApiHost
{
    using Microsoft.AspNetCore.Mvc;

    public abstract class ApiControllerBase : Controller
    {
        protected int OrganizationId
        {
            get
            {
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
