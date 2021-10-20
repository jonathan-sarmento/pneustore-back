using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using pneustoreapi.Models;
using pneustoreAPI.Models;
using pneustoreAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace pneustoreAPI.Controllers
{
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class AuthController : APIBaseController
    {
        private readonly IAuthService<PneuUser> _authService;
        private readonly CarrinhoService _carrinhoService;
        public AuthController(IAuthService<PneuUser> authService, CarrinhoService carrinhoService)
        {
            _authService = authService;
            _authService.TimeHasExpired();
            _carrinhoService = carrinhoService;
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

        [HttpPost, Route("Register")]
        public IActionResult NewUser(PneuUser identityUser)
        {
            try
            {
                IdentityResult result = _authService.Create(identityUser).Result;
                if (!result.Succeeded) throw new Exception();

                // Verifica se o ususário não é anonimo e faz a migração dos carrinhos para o usuario real
                if(!identityUser.IsAnonymous && identityUser.UserName != identityUser.IP){
                    var listCarrinhos = _carrinhoService.GetFromUser(identityUser.IP);
                    
                    if(listCarrinhos.Any())
                        listCarrinhos.ForEach(c => _carrinhoService.Create(new Carrinho()
                        {
                            Quantity = c.Quantity,
                            ProductId = c.ProductId,
                            UserId = _carrinhoService.GetCurrentUserByUsername(identityUser.UserName).Id
                        }));
                }

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
        [HttpPost, Route("Token")]
        
        public IActionResult Token([FromBody] PneuUser identityUser)
        {
            try
            {
                return ApiOk(_authService.GenerateToken(identityUser));
            }
            catch (Exception exception)
            {
                return ApiBadRequest(exception.Message);
            }
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost, Route("DeleteUser")]
        public IActionResult DeleteUser(string id, Exception exception)
        {
            if (id == null)
                return ApiBadRequest(exception.Message);
            
            return ApiOk(_authService.DeleteUser(id));
            
        }
      
    }
}
