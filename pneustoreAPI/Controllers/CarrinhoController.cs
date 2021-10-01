using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    [Authorize]
    public class CarrinhoController : APIBaseController
    {
        CarrinhoService service;
        public CarrinhoController(CarrinhoService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return ApiOk(service.GetAll());
        }

        [HttpGet]
        [Route("/FromUser")]
        public IActionResult GetFromUser()
        {
            return ApiOk(service.GetFromUser(User.Identity.Name));
        }

        [HttpGet]
        [Route("{productId}")]
        public IActionResult Index(int? productId)
        {
            return ApiOk(service.Get(User.Identity.Name, productId));
        }


        [Route("{id}")]
        [HttpDelete]
        public IActionResult Delete(int? id) =>
         service.Delete(User.Identity.Name, id) ?
               ApiOk("Carrinho deletado com sucesso!") :
               ApiNotFound("Erro ao deletar carrinho!");

        [HttpPost]
        public IActionResult AddCarrinho([FromBody] Carrinho carrinho) =>
            service.Create(carrinho) ?
                ApiCreated($"[controller]/{service.GetAll().LastOrDefault()}", "Carrinho criado com sucesso.")
                : ApiBadRequest(carrinho, "Deu erro");
    }
}
