namespace ApiHost
{
    using Microsoft.AspNetCore.Mvc;

    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;

    public abstract class ApiControllerBase : Controller
    {
        private JwtSecurityToken? JwtSecurityToken
        {
            get
            {
                var item = HttpContext.Items["JwtSecurityToken"];
                if (item == null) return null;

                return (JwtSecurityToken)item;
            }
        }

        protected int ResellerId
        {
            get
            {
                var token = this.JwtSecurityToken;
                if (token == null) return 0;

                var organizationId = token.Claims.SingleOrDefault(x => x.Type.Trim().ToLower().StartsWith("resellerid"));
                if (organizationId == null) return 0;
                return int.Parse(organizationId.Value);
            }
        }

        protected int OrganizationId
        {
            get
            {
                var token = this.JwtSecurityToken;
                if (token == null) return 1;

                var organizationId = token.Claims.SingleOrDefault(x => x.Type.Trim().ToLower().StartsWith("organizationid"));
                if (organizationId == null) return 1;
                return int.Parse(organizationId.Value);
            }
        }

        protected string UserName
        {
            get
            {
                var token = this.JwtSecurityToken;
                if (token == null) return string.Empty;

                var userName = token.Claims.SingleOrDefault(x => x.Type.Trim().ToLower().StartsWith("username"));
                if (userName == null) return string.Empty;
                return userName.Value.ToString();
            }
        }

        protected string ipAddress()
        {
            string? result;

            // get source ip address for the current request
            if (this.Request.Headers.ContainsKey("X-Forwarded-For"))
            {
                result = this.Request.Headers["X-Forwarded-For"];
            }
            else
            {
                result = this.HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString();
            }

            return result ?? string.Empty;
        }
    }
}
