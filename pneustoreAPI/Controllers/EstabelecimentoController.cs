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
        public IActionResult Get(int? id)
            => _service.Get(id) != null ?
                    ApiOk(_service.Get(id)) :
                    ApiNotFound($"Estabelecimento com o id:{id} não existe.");

        /// <summary>
        /// Cria um registro de um estabelecimento no banco de dados
        /// </summary>
        /// <param name="estabelecimento"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create([FromBody] Estabelecimento estabelecimento) 
            => _service.Create(estabelecimento) ?
                    ApiCreated("Estabelecimento criado com sucesso!") :
                    ApiBadRequest("Não foi possível criar o estabelecimento.");

        /// <summary>
        /// Atualiza o registro do estabelecimento específico no banco de dados
        /// </summary>
        /// <param name="estabelecimento"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Update([FromBody] Estabelecimento estabelecimento)
            => _service.Update(estabelecimento) ? 
                    ApiOk("Estabelecimento atualizado com sucesso!") :
                    ApiBadRequest("Não foi possível atualizar o estabelecimento!");

        /// <summary>
        /// Deleta o registro de um estabelecimento específico no banco de dados.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int? id)
           => _service.Delete(_service.Get(id)) ?
                ApiOk("Estabelecimento deletado com sucesso!") :
                ApiBadRequest("Não foi possível deletar o estabelecimento!");

        /// <summary>
        /// Devolve todos os estoques existentes de um produto específico.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("Estoque/{id}"), HttpGet]
        public IActionResult GetEstoque(int? id) {
            var estoque = _service.GetEstoque(id);
            return estoque.Count > 0 ? ApiOk(estoque) : ApiNotFound($"Não há estoque para o produto com id {id}.");
        }

        /// <summary>
        /// Devolves os estoques presentes no banco de dados.
        /// </summary>
        /// <returns></returns>
        [Route("Estoque"), HttpGet]
        public IActionResult GetAllEstoque()
        {
            var estoques = _service.GetAllEstoque();
            return estoques.Count > 0 ? ApiOk(estoques) : ApiNotFound("Não há estoques!");
        }

        /// <summary>
        /// Devolve um estoque de um produto e estabelecimento específico.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="estabId"></param>
        /// <returns></returns>
        [Route("Estoque/{productId?}/{estabId?}"), HttpGet]
        public IActionResult GetSingleEstoque(int? productId, int? estabId) {
            EstabPneu retorno = _service.GetSingleEstoque(productId, estabId);
            return retorno.ProductId != 0 && retorno.EstabelecimentoId != 0 ? 
                ApiOk(retorno) : 
                ApiBadRequest($"Não há produtos de id {productId} no estabelecimento {estabId} com estoque!");
        }

        /// <summary>
        /// Cria o registro de um estoque de um produto em um determinado estabelecimento. 
        /// </summary>
        /// <param name="estoque"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Estoque")]
        public IActionResult CreateEstoque([FromBody] EstabPneu estoque)
            => _service.CreateEstoque(estoque) ? 
                    ApiCreated("Estoque criado com sucesso!") :
                    ApiBadRequest(estoque, "Não foi possível criar o estoque!");

        /// <summary>
        /// Atualizar o estoque de um produto em um estabelecimento.
        /// </summary>
        /// <param name="estoque"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Estoque")]
        public IActionResult UpdateEstoque([FromBody] EstabPneu estoque)
            => _service.EditEstoque(estoque) ?
                    ApiOk("Estoque atualizado com sucesso!") :
                    ApiBadRequest(estoque, "Não foi possível atualizar o estoque!");

        /// <summary>
        /// Deletar o estoque de um produto em um estabelecimento.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="estabId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Estoque/{productId?}/{estabId?}")]
        public IActionResult DeleteEstoque(int? productId, int? estabId)
            => _service.DeleteEstoque(_service.GetSingleEstoque(productId, estabId)) ? 
                    ApiOk("Estoque deletado com sucesso!") : 
                    ApiBadRequest("Não foi possível deletar o estoque!");
    }
}