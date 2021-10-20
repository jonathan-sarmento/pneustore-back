using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pneustoreAPI.API;
using pneustoreAPI.Data;
using pneustoreAPI.Models;
using pneustoreAPI.Services;
using System;
using System.Linq;

namespace pneustoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[AuthorizeRoles(Roles ="Admin")]
    //[] rota para permitir Role.admin
    public class CupomController : APIBaseController
    {
        private readonly ICupomService _cupomService;
        public CupomController(ICupomService cupomService)
        {
            _cupomService = cupomService;
        }

        /// <summary>
        /// Retorna todos os cupons no banco de dados.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index() 
            => ApiOk(_cupomService.GetAll());

        /// <summary>
        /// Cria um cupom no banco de dados.
        /// </summary>
        /// <param name="cupom"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(Cupom cupom)
        {
            if(cupom.Desconto < 1 && cupom.Desconto > 0) { 
                try
                {
                    _cupomService.Create(cupom);
                    return ApiCreated("Cupom criado");
                }
                catch (Exception ex)
                {
                    return ApiBadRequest(ex.Message,"Houve um erro na criação de cupom.");
                }
            }
            return ApiBadRequest("Valor de desconto inválido! Insira um valor entre 0 e 1.");
        }

        /// <summary>
        /// Retorna um cupom específico com base de seu conteúdo.
        /// </summary>
        /// <param name="nome"></param>
        /// <returns></returns>
        [HttpGet, Route("Get")]
        public IActionResult Get([FromBody] string nome)
        {
            var cupom = _cupomService.Get(nome);
            return cupom != null ? ApiOk(cupom) : ApiBadRequest("Não existem cupons com esse conteúdo!");
        }

        /// <summary>
        /// Deleta um cupom com base em seu ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete, Route("{id?}")]
        public IActionResult Delete(int? id)
        {
            try
            {
                _cupomService.Delete(id);
                return ApiOk("Cupom deletado!");
            }
            catch(Exception ex)
            {
                return ApiBadRequest(ex.Message, "Houve um erro na criação de cupom.");
            }
        }
    }
}
