using Microsoft.AspNetCore.Mvc;
using pneustoreAPI.Models;
using pneustoreAPI.Services;

namespace pneustoreAPI.Controllers
{
    public class EstabelecimentoController : APIBaseController
    {
        IService<Estabelecimento> _service;
        public EstabelecimentoController(IService<Estabelecimento> service)
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
    }
}