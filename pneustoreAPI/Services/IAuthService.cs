using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pneustoreAPI.Services
{
    public interface IAuthService
    {
        IdentityUser GetUser(IdentityUser identityUser);

        Task<SignInResult> ValidateUser(IdentityUser identityUser);

        Task<IdentityResult> Create(IdentityUser identityUser);

        string GetUserRole(IdentityUser identityUser);

        string GenerateToken(IdentityUser identityUser);
    }
}
