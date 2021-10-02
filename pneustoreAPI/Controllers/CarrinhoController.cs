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
        IService<Product> _productService;
        public CarrinhoController(CarrinhoService service, IService<Product> productService)
        {
            this.service = service;
            _productService = productService;
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


        [HttpPut]
        public IActionResult Update([FromBody] Carrinho prod)
        {
            return service.Update(prod) ?
                ApiOk("Carrinho atualizado com sucesso!") :
                ApiNotFound("Erro ao atualizar carrinho!");
        }


        [Route("{id}")]
        [HttpDelete]
        public IActionResult Delete(int? id) =>
         service.Delete(User.Identity.Name, id) ?
               ApiOk("Carrinho deletado com sucesso!") :
               ApiNotFound("Erro ao deletar carrinho!");

        [HttpPost]
        [Route("{produtoId}")]
        public IActionResult AddCarrinho(int produtoId) {
            Carrinho carrinho = new Carrinho()
            {
                ProductId = produtoId,
                UserId = service.GetCurrentUserId(User.Identity.Name),
                Product = _productService.Get(produtoId)
            };
            return service.Create(carrinho) ?
                ApiCreated($"[controller]/{service.GetAll().LastOrDefault()}", "Carrinho criado com sucesso.")
                : ApiBadRequest(carrinho, "Deu erro");
        }
    }
}
