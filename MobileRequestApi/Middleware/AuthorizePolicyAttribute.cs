namespace APISupport
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    [AttributeUsage(AttributeTargets.Method)]
    public class AllowAnonymousPolicyAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizePolicyAttribute : Attribute,
                                      IAuthorizationFilter
    {
        public string Policy { get; set; } = string.Empty;

        public AuthorizePolicyAttribute(string policy)
        {
            this.Policy = policy;
        }

        public AuthorizePolicyAttribute()
        {
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                var jwtSecurityToken = (JwtSecurityToken?)context.HttpContext.Items["JwtSecurityToken"];

                // skip authorization if action is decorated with [AllowAnonymous] attribute
                var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousPolicyAttribute>().Any();
                if (allowAnonymous)
                {
                    return;
                }

                if (string.IsNullOrEmpty(Policy))
                {
                    return;
                }

                // No token - fail
                if (jwtSecurityToken == null)
                {
                    context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status403Forbidden };
                    return;
                }

                // no claims
                if (jwtSecurityToken.Claims == null)
                {
                    context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status403Forbidden };
                    return;
                }

                // no claims or not matching claim
                var roles = jwtSecurityToken.Claims.Where(x => x.Type.Trim().ToLower() == "roles");
                if (roles == null)
                {
                    context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status403Forbidden };
                    return;
                }

                var policiesRequired = this.Policy.Split(',');
                var found = false;
                foreach (var policy in policiesRequired)
                {
                    foreach (var role in roles)
                    {
                        if (role.Value.Trim().ToLower().Contains(policy.Trim().ToLower()))
                        {
                            found = true;
                            break;
                        }
                    }
                }

                if (!found)
                {
                    context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status403Forbidden };
                    return;
                }
            }
            catch
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status403Forbidden };
            }
        }
    }
}