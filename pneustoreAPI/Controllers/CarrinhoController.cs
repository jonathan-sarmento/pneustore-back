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

        /// <summary>
        /// Retorna todos os produtos presentes nos carrinhos de usuários.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
            => ApiOk(_service.GetAll());
            
        /// <summary>
        /// Retorna todos os produtos presente no carrinho do usuário.
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetFromUser")]
        public IActionResult GetFromUser()
        {
            var carrinhoList = _service.GetFromUser(User.Identity.Name);

            return carrinhoList.Count == 0 ? ApiBadRequest(carrinhoList, "Não há itens no carrinho!") : ApiOk(carrinhoList);
        }

        /// <summary>
        /// Retorna um produto e sua informação armazenada no carrinho.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpGet, Route("{productId}")]
        public IActionResult Get(int? productId)
            => ApiOk(_service.Get(User.Identity.Name, productId));

        /// <summary>
        /// Atualiza um produto e sua informação armazenada no carrinho do usuário.
        /// </summary>
        /// <param name="prod"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Update([FromBody] Carrinho prod)
            => _service.Update(prod) ?
                    ApiOk("Carrinho atualizado com sucesso!") :
                    ApiNotFound("Erro ao atualizar carrinho!");

        /// <summary>
        /// Deleta um produto do carrinho do usuário.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete, Route("{id}")]
        public IActionResult Delete(int? id)
            => _service.Delete(User.Identity.Name, id) ?
                    ApiOk("Carrinho deletado com sucesso!") :
                    ApiNotFound("Erro ao deletar carrinho!");

        /// <summary>
        /// Adiciona um produto ao carrinho do usuário.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Retorna o custo total de todos os produtos presentes no carrinho. Pode ser aplicado cupons.
        /// </summary>
        /// <param name="cupom"></param>
        /// <returns></returns>
        [HttpGet, Route("TotalPreco")]
        public IActionResult GetTotalPreco([FromBody] string cupom) {
            var total = _service.TotalCarrinho(User.Identity.Name);

            //Feito para evitar realizar uma consulta quando há certeza que não achará nada.
            if(cupom != "") total = (double)total - (double)(total * _cupomService.Get(cupom).Desconto);

            var totalFinal = total;
            return totalFinal > 0 ? ApiOk(Math.Round((decimal)totalFinal,3)) : ApiBadRequest("Não há itens no carrinho!"); 
        }
    }
}