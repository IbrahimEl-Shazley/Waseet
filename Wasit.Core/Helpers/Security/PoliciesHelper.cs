using Microsoft.AspNetCore.Authorization;

namespace Wasit.Core.Helpers.Security
{
    public static class PoliciesHelper
    {
        public static AuthorizationPolicy CreatePolicy(params string[] roles)
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(roles).Build();
        }
    }
}
