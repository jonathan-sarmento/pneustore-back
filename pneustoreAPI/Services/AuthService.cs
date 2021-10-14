using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using pneustoreapi.Models;
using pneustoreAPI.API;
using pneustoreAPI.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace pneustoreAPI.Services
{
    public class AuthService : IAuthService<PneuUser>
    {
        IConfiguration _config;
        UserManager<PneuUser> _userManager;
        SignInManager<PneuUser> _signInManager;
        public AuthService(UserManager<PneuUser> userManager, SignInManager<PneuUser> signInManager, IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
        }
        public PneuUser GetUser(PneuUser identityUser)
        {
            var user = _userManager.FindByNameAsync(identityUser.UserName).Result;
            var valid = _signInManager.CheckPasswordSignInAsync(user, identityUser.PasswordHash, false);
            return valid.Result.Succeeded ? user : null;
        }

        public async Task<SignInResult> ValidateUser(PneuUser identityUser)
        {
            var user = await _userManager.FindByNameAsync(identityUser.UserName);
            var valid = await _signInManager.CheckPasswordSignInAsync(user, identityUser.PasswordHash, false);
            return valid;
        }
        public async Task<IdentityResult> Create(PneuUser identityUser)
        {
            identityUser.Created = DateTime.Now;
    
            var result = await _userManager.CreateAsync(identityUser, identityUser.PasswordHash);
            if (result.Succeeded)
            {
                var user = _userManager.FindByNameAsync(identityUser.UserName);
                await _userManager.AddToRoleAsync(user.Result, Enum.GetName(default(RoleType)));
            }
            return result;
        }
        public string GetUserRole(PneuUser identityUser)
        {
            var rolename = _userManager.GetRolesAsync(identityUser);
            return rolename.Result[0];
        }

        public string GenerateToken(PneuUser identityUser)
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
                Expires = user.IsAnonymous ? DateTime.UtcNow.AddMinutes(10) : DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public IdentityResult DeleteUser(string id)
        {
            var user = _userManager.FindByIdAsync(id).Result;
            var result = _userManager.DeleteAsync(user).Result;
            if (result.Succeeded) { }
            return result;
        }

        public async Task<IList<PneuUser>> GetAllUsersAsync(){

            return await _userManager.GetUsersInRoleAsync(nameof(RoleType.Admin));
        }

         public void TimeHasExpired(){
            
            var users = GetAllUsersAsync().Result;

            var anonymousUsers = users.Where(u => u.IsAnonymous &&  (DateTime.Now - u.Created).TotalMinutes >= 10);

            if (anonymousUsers.Any())
                anonymousUsers.ToList().ForEach(u => DeleteUser(u.Id));
        }
    }
}
