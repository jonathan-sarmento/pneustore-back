using Microsoft.AspNetCore.Mvc;
using pneustoreAPI.API;
using pneustoreAPI.Models;
using pneustoreAPI.Services;

namespace pneustoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EstabelecimentoController : APIBaseController
    {
        EstabelecimentoService _service;
        public EstabelecimentoController(EstabelecimentoService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retorna todos os estabelecimentos cadastrados no banco de dados
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index() => ApiOk(_service.GetAll());
        /// <summary>
        /// Retorna estabelecimento específico a partir de ID único
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [Route("{id}"), HttpGet]
        [AuthorizeRoles(RoleType.Common)]
        public IActionResult Get(int? id) =>
            _service.Get(id) != null ?
                ApiOk(_service.Get(id)) :
                ApiNotFound($"Estabelecimento com o id:{id} não existe.");

        [Route("Estoque/{id}"), HttpGet]
        public IActionResult GetEstoque(int? id) {
            var estoque = _service.GetEstoque(id);
            return estoque != null ? ApiOk(estoque) : ApiNotFound($"Não há estoque para o produto com id {id}.");
        }

        [HttpPost]
        public IActionResult CreateEstoque([FromBody] EstabPneu estoque)
        {
            return _service.CreateEstoque(estoque) ? 
                ApiOk("Estoque criado com sucesso!") :
                ApiBadRequest(estoque, "Não foi possível criar o produto!");
        }

        [HttpPost]
        public IActionResult Create([FromBody] Estabelecimento estabelecimento)
        {
            return _service.Create(estabelecimento) ? 
                ApiOk("Estabelecimento criado com sucesso!") : 
                ApiBadRequest("Não foi possível criar o estabelecimento.");
        }
    }
}