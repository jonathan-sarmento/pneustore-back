using Microsoft.AspNetCore.Authorization;
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
        public IActionResult Get(int? id) =>
            _service.Get(id) != null ?
                ApiOk(_service.Get(id)) :
                ApiNotFound($"Estabelecimento com o id:{id} não existe.");

        [HttpPost]
        public IActionResult Create([FromBody] Estabelecimento estabelecimento)
        {
            return _service.Create(estabelecimento) ?
            ApiOk("Estabelecimento criado com sucesso!") :
            ApiBadRequest("Não foi possível criar o estabelecimento.");
        }

        [HttpPut]
        public IActionResult Update([FromBody] Estabelecimento estabelecimento)
        {
            return _service.Update(estabelecimento) ? 
                ApiOk("Estabelecimento atualizado com sucesso!") :
                ApiBadRequest("Não foi possível atualizar o estabelecimento!");
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int? id)
        {
            return _service.Delete(_service.Get(id)) ?
                ApiOk("Estabelecimento deletado com sucesso!") :
                ApiBadRequest("Não foi possível deletar o estabelecimento!");
        }

        [Route("Estoque/{id}"), HttpGet]
        public IActionResult GetEstoque(int? id) {
            var estoque = _service.GetEstoque(id);
            return estoque.Count > 0 ? ApiOk(estoque) : ApiNotFound($"Não há estoque para o produto com id {id}.");
        }

        [Route("Estoque"), HttpGet]
        public IActionResult GetAllEstoque()
        {
            var estoques = _service.GetAllEstoque();
            return estoques.Count > 0 ? ApiOk(estoques) : ApiNotFound("Não há estoques!");
        }

        [Route("Estoque/{productId?}/{estabId?}"), HttpGet]
        public IActionResult GetSingleEstoque(int? productId, int? estabId)
        {
            return ApiOk(_service.GetSingleEstoque(productId, estabId));
        }

        [HttpPost]
        [Route("Estoque")]
        public IActionResult CreateEstoque([FromBody] EstabPneu estoque)
        {
            return _service.CreateEstoque(estoque) ? 
                ApiOk("Estoque criado com sucesso!") :
                ApiBadRequest(estoque, "Não foi possível criar o estoque!");
        }

        [HttpPut]
        [Route("Estoque")]
        public IActionResult UpdateEstoque([FromBody] EstabPneu estoque)
        {
            return _service.EditEstoque(estoque) ?
                ApiOk("Estoque atualizado com sucesso!") :
                ApiBadRequest(estoque, "Não foi possível atualizar o estoque!");
        }

        [HttpDelete]
        [Route("Estoque/{productId?}/{estabId?}")]
        public IActionResult DeleteEstoque(int? productId, int? estabId)
        {
            return _service.DeleteEstoque(_service.GetSingleEstoque(productId, estabId)) ? 
                ApiOk("Estoque deletado com sucesso!") : 
                ApiBadRequest("Não foi possível atualizar o estoque!");
        }
    }
}