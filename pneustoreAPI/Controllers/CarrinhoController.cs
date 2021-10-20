using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pneustoreapi.Models;
using pneustoreAPI.Models;
using pneustoreAPI.Services;
using System;
using System.Linq;
using System.Net.Mime;

namespace pneustoreAPI.Controllers
{
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class CarrinhoController : APIBaseController
    {
        private readonly CarrinhoService _service;
        private readonly IService<Product> _productService;
        private readonly ICupomService _cupomService;
        private readonly IAuthService<PneuUser> _authService;
        public CarrinhoController(CarrinhoService service, IService<Product> productService, IAuthService<PneuUser> authService, ICupomService cupomService)
        {
            _service = service;
            _productService = productService;
            _authService = authService;
            _cupomService = cupomService;
            _authService.TimeHasExpired();
        }

        [HttpGet]
        public IActionResult Index()
            => ApiOk(_service.GetAll());
            

        [HttpGet, Route("GetFromUser")]
        public IActionResult GetFromUser()
        {
            var carrinhoList = _service.GetFromUser(User.Identity.Name);

            return carrinhoList.Count == 0 ? ApiBadRequest(carrinhoList, "Não há itens no carrinho!") : ApiOk(carrinhoList);
        }

        [HttpGet, Route("{productId}")]
        public IActionResult Get(int? productId)
            => ApiOk(_service.Get(User.Identity.Name, productId));


        [HttpPut]
        public IActionResult Update([FromBody] Carrinho prod)
            => _service.Update(prod) ?
                    ApiOk("Carrinho atualizado com sucesso!") :
                    ApiNotFound("Erro ao atualizar carrinho!");

        [HttpDelete, Route("{id}")]
        public IActionResult Delete(int? id)
            => _service.Delete(User.Identity.Name, id) ?
                    ApiOk("Carrinho deletado com sucesso!") :
                    ApiNotFound("Erro ao deletar carrinho!");

        [HttpPost]
        public IActionResult AddCarrinho([FromBody, Bind(include:new string[]{"Quantity", "ProductId"})] Carrinho model) {
            Carrinho carrinho = new Carrinho()
            {
                Quantity = model.Quantity,
                ProductId = model.ProductId,
                UserId = _service.GetCurrentUserByUsername(User.Identity.Name).Id
            };

            return _service.Create(carrinho) ?
                ApiCreated("Carrinho criado com sucesso.")
                : ApiBadRequest(carrinho, "Deu erro");
        }

        [HttpGet, Route("TotalPreco")]
        public IActionResult GetTotalPreco([FromBody]string cupom) {
            var total = _service.TotalCarrinho(User.Identity.Name);
            var totalFinal = (decimal)total - (decimal)(total * _cupomService.Get(cupom).Desconto);

            return totalFinal > 0 ? ApiOk(Math.Round(totalFinal,3)) : ApiBadRequest("Não há itens no carrinho!"); 
        }
    }
}