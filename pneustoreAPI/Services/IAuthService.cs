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
        public T GetUser(T identityUser);

        T GetUserByUsername(string username);

        public Task<SignInResult> ValidateUser(T identityUser);

        public Task<IdentityResult> Create(T identityUser);
        public /*Task<IdentityResult>*/ IdentityResult DeleteUser(string id);

        public string GetUserRole(T identityUser);

        public string GenerateToken(T identityUser);

        void TimeHasExpired();
    }
}
