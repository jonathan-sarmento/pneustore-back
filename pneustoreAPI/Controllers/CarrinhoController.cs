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
        [Route("GetFromUser")]
        public IActionResult GetFromUser()
        {
            var carrinhoList = service.GetFromUser(User.Identity.Name);
            // List<Carrinho> list = new();
            // carrinhoList.ForEach(c => list.Add(new Carrinho()
            // {
            //     Quantity = c.Quantity,
            //     ProductId = c.ProductId,
            //     Product = c.Product,
            //     UserId = c.UserId
            // }));

            return ApiOk(carrinhoList);
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
        public IActionResult AddCarrinho([FromBody, Bind(include:new string[]{"Quantity", "ProductId"})] Carrinho model) {
            Carrinho carrinho = new Carrinho()
            {
                Quantity = model.Quantity,
                ProductId = model.ProductId,
                UserId = service.GetCurrentUserId(User.Identity.Name)
            };

            return service.Create(carrinho) ?
                ApiCreated($"[controller]/Add/{service.GetAll().LastOrDefault()}", "Carrinho criado com sucesso.")
                : ApiBadRequest(carrinho, "Deu erro");
        }
    }
}
