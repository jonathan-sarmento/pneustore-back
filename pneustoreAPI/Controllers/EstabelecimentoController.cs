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

        [HttpGet]
        public IActionResult Index() => ApiOk(_service.GetAll());

        [Route("{id}"), HttpGet]
        public IActionResult Get(int? id) =>
            _service.Get(id) != null ?
                ApiOk(_service.Get(id)) :
                ApiNotFound($"Estabelecimento com o id:{id} não existe.");
    }
}