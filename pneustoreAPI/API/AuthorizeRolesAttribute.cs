using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pneustoreAPI.API
{
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params RoleType[] r)
        {
            var roles = r.Select(x => Enum.GetName(typeof(RoleType), x));
            Roles = string.Join(",", roles);
        }
    }
}
