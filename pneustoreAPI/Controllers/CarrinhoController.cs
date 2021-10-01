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
        CarrinhoServices service;
        public CarrinhoController(CarrinhoServices service)
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

        [HttpPost]
        public IActionResult AddCarrinho([FromBody] Carrinho carrinho) =>
            service.Create(carrinho) ?
                ApiCreated($"[controller]/{service.GetAll().LastOrDefault()}", "Carrinho criado com sucesso.")
                : ApiBadRequest(carrinho, "Deu erro");
    }
}
