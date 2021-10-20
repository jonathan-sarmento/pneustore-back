using Microsoft.AspNetCore.Identity;
using pneustoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pneustoreAPI.Services
{
    public interface IAuthService<T>
    {
        T GetUser(T identityUser);

        Task<SignInResult> ValidateUser(T identityUser);

        Task<IdentityResult> Create(T identityUser);
        IdentityResult DeleteUser(string id);

        string GetUserRole(T identityUser);

        string GenerateToken(T identityUser);

        void TimeHasExpired();
    }
}
