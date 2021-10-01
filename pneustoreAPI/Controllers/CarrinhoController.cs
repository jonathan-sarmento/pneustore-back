using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    [Authorize]

    public class CarrinhoController : APIBaseController
    {
        CarrinhoServices services;
        public IActionResult Index()
        {
            return Ok();
        }

        //[Route("{id}")]
        //[HttpGet]
        //public IActionResult AddCarrinho(int id)
        //{

        //}

        [Route("{id}")]
        [HttpDelete]
        public IActionResult Delete(int? id) =>
         services.Delete(id) ?
               ApiOk("Carrinho deletado com sucesso!") :
               ApiNotFound("Erro ao deletar carrinho!");
    }
}
