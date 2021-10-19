using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pneustoreAPI.API;
using pneustoreAPI.Data;
using pneustoreAPI.Models;
using pneustoreAPI.Services;
using System.Linq;

namespace pneustoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[AuthorizeRoles(Roles ="Admin")]
    //[] rota para permitir Role.admin
    public class CupomController : APIBaseController
    {
        ICupomService _cupomService;
        public CupomController(ICupomService cupomService)
        {
            _cupomService = cupomService;
        }

        [HttpGet,Route("Cupons")]
        public IActionResult Index() => ApiOk(_cupomService.GetAll());

        [HttpPost, Route("Create")]

        public IActionResult Create(Cupom cupom)
        {
            //var exists = _cupomService.Get(cupom.Nome);
            try
            {
                _cupomService.Create(cupom);
                return ApiOk("Cupom criado");
            }
            catch
            {
                return ApiBadRequest("Erro");
            }
        }
        [HttpGet, Route("Get")]

        public IActionResult Get([FromBody]string nome)
        {
              return ApiOk(_cupomService.Get(nome));
        }


        [HttpPost, Route("Delete")]
        public IActionResult Delete(int? id)
        {
            try
            {
                _cupomService.Delete(id);
                return ApiOk("Deletado");
            }
            catch
            {
                return ApiBadRequest("Erro");
            }


        }
    }
}
