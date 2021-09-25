using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using pneustoreAPI.API;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace pneustoreAPI.Services
{
    public class AuthService : IAuthService
    {
        IConfiguration _config;
        UserManager<IdentityUser> _userManager;
        SignInManager<IdentityUser> _signInManager;
        public AuthService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
        }
        public IdentityUser GetUser(IdentityUser identityUser)
        {
            var user = _userManager.FindByNameAsync(identityUser.UserName).Result;
            var valid = _signInManager.CheckPasswordSignInAsync(user, identityUser.PasswordHash, false);
            return valid.Result.Succeeded ? user : null;
        }

        public async Task<SignInResult> ValidateUser(IdentityUser identityUser)
        {
            var user = await _userManager.FindByNameAsync(identityUser.UserName);
            var valid = await _signInManager.CheckPasswordSignInAsync(user, identityUser.PasswordHash, false);
            return valid;
        }
        public async Task<IdentityResult> Create(IdentityUser identityUser)
        {
            var result = await _userManager.CreateAsync(identityUser, identityUser.PasswordHash);
            if (result.Succeeded)
            {
                var user = _userManager.FindByNameAsync(identityUser.UserName);
                await _userManager.AddToRoleAsync(user.Result, Enum.GetName(default(RoleType)));
            }
            return result;
        }
        public string GetUserRole(IdentityUser identityUser)
        {
            var rolename = _userManager.GetRolesAsync(identityUser);
            return rolename.Result[0];
        }

        public string GenerateToken(IdentityUser identityUser)
        {
            var user = GetUser(identityUser);
            var role = GetUserRole(user);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName.ToString()),
                    new Claim(ClaimTypes.Role, role),
                    new Claim(ClaimTypes.NameIdentifier, user.Id)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
           }

       
    }
}
