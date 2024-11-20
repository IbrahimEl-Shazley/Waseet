namespace Wasit.Helpers
{
    public class AuthorizeRolesAttribute : Microsoft.AspNetCore.Authorization.AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params Wasit.Core.Enums.Roles[] roles) : base()
        {
            Roles = string.Join(",", roles);
        }
    }
}
