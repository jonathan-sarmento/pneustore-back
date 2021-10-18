using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pneustoreapi.Models;
using pneustoreAPI.Models;
using pneustoreAPI.Services;
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
        CarrinhoService service;
        IService<Product> _productService;
        IAuthService<PneuUser> _authService;
        public CarrinhoController(CarrinhoService service, IService<Product> productService, IAuthService<PneuUser> authService)
        {
            _service = service;
            _productService = productService;
            _authService = authService;
            _cupomService = cupomService;
            _authService.TimeHasExpired();
        }

        [HttpGet]
        public IActionResult Index()
        {
            return ApiOk(_service.GetAll());
        }

        [HttpGet]
        [Route("GetFromUser")]
        public IActionResult GetFromUser()
        {
            var carrinhoList = _service.GetFromUser(User.Identity.Name);

            return carrinhoList.Count == 0 ? ApiBadRequest(carrinhoList, "Não há itens no carrinho!") : ApiOk(carrinhoList);
        }

        [HttpGet]
        [Route("{productId}")]
        public IActionResult Index(int? productId)
        {
            return ApiOk(_service.Get(User.Identity.Name, productId));
        }


        [HttpPut]
        public IActionResult Update([FromBody] Carrinho prod)
        {
            return _service.Update(prod) ?
                ApiOk("Carrinho atualizado com sucesso!") :
                ApiNotFound("Erro ao atualizar carrinho!");
        }

        [Route("{id}")]
        [HttpDelete]
        public IActionResult Delete(int? id) =>
         _service.Delete(User.Identity.Name, id) ?
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

            return service.Create(carrinho) ?
                ApiCreated($"[controller]/Add/{service.GetAll().LastOrDefault()}", "Carrinho criado com sucesso.")
                : ApiBadRequest(carrinho, "Deu erro");
        }

        [HttpGet, Route("TotalPreco")]
        public IActionResult GetTotalPreco() { 
           var total = service.TotalCarrinho(User.Identity.Name);
            return total > 0 ? ApiOk(total) : ApiBadRequest("Não há itens no carrinho!");
        }

        }
    }
}
