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
        public T GetUser(string userName, T identityUser);

        public Task<SignInResult> ValidateUser(T identityUser);

        public Task<IdentityResult> Create(T identityUser);
        public Task<IdentityResult> DeleteUser(T identityUser);

        public string GetUserRole(T identityUser);

        public string GenerateToken(T identityUser);
        object GetUser(string userName, string passwordHash);
    }
}
