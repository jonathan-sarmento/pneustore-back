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
        public PneuUser GetUser(PneuUser identityUser);

        public Task<SignInResult> ValidateUser(PneuUser identityUser);

        public Task<IdentityResult> Create(PneuUser identityUser);

        public string GetUserRole(PneuUser identityUser);

        public string GenerateToken(PneuUser identityUser);
    }
}
