namespace ApiHost
{
    using System.Data;

    using Microsoft.AspNetCore.Mvc;

    public abstract class ApiControllerBase : Controller
    {
        protected int OrganizationId
        {
            get
            {
                var value = this.HttpContext.Items["OrganizationId"];
                if (value == null)
                {
                    throw new NoNullAllowedException("OrganizationId is null");
                }

                return (int)value;
            }
        }

        protected int UserId
        {
            get
            {
                var value = this.HttpContext.Items["UserId"];
                if (value == null)
                {
                    throw new NoNullAllowedException("OrganizationId is null");
                }

                return (int)value;
            }
        }
    }
}
