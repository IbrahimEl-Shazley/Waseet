﻿using Microsoft.AspNetCore.Identity;
using Wasit.Core.Enums;

namespace Wasit.Context.Seeds
{
    public static class DefaultRoles
    {
        public static List<IdentityRole> IdentityRoleList()
        {
            List<IdentityRole> identityRoles = new List<IdentityRole>();
            foreach (Roles role in (Roles[])Roles.GetValues(typeof(Roles)))
            {
                identityRoles.Add(new IdentityRole(role.ToString()));
            }

            return identityRoles;
        }
    }
}
