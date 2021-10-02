using Microsoft.AspNetCore.Identity;
using pneustoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pneustoreAPI.Services
{
    public interface IAuthService
    {
        public IdentityUser GetUser(IdentityUser identityUser);

        public Task<SignInResult> ValidateUser(IdentityUser identityUser);

        public Task<IdentityResult> Create(IdentityUser identityUser);

        public string GetUserRole(IdentityUser identityUser);

        public string GenerateToken(IdentityUser identityUser);
    }
}
