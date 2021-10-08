using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using pneustoreAPI.Services;
using System;
using System.Net.Mime;

namespace pneustoreAPI.Controllers
{
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class AuthController : APIBaseController
    {
        IAuthService<IdentityUser> service;
        public AuthController(IAuthService<IdentityUser> service)
        {
            this.service = service;
        }
        

        #region sign up swagger comments
        /// <summary>
        /// Cria um novo registro no banco de dados com as informações providenciadas pelo usuário para autenticação.
        /// </summary>
        /// <param name="identityUser"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        [HttpPost]
        [Route("Register")]
        public IActionResult NewUser(IdentityUser identityUser)
        {
            try
            {
                IdentityResult result = service.Create(identityUser).Result;
                if (!result.Succeeded) throw new Exception();
                identityUser.PasswordHash = "";
                return ApiOk(identityUser);
            }
            catch
            {
                return ApiBadRequest("Erro ao criar usuário!");
            }
        }

        #region token creation swagger comments
        /// <summary>
        /// Cria um token para o usuário com base nas informações providenciadas pelo mesmo.
        /// </summary>
        /// <param name="identityUser"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        #endregion
        [HttpPost]
        [Route("Token")]
        
        public IActionResult Token([FromBody] IdentityUser identityUser)
        {
            try
            {
                return ApiOk(service.GenerateToken(identityUser));
            }
            catch (Exception exception)
            {
                return ApiBadRequest(exception.Message);
            }
        }
  
    }
}
