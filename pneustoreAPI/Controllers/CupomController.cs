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
    [AuthorizeRoles(Roles ="Admin")]
    //[] rota para permitir Role.admin
    public class CupomController : APIBaseController
    {
        ICupomService _cupomService;
        public CupomController(ICupomService cupomService)
        {
            _cupomService = cupomService;
        }
        [HttpGet]

        public IActionResult Index() => ApiOk("100% de desconto pra vc");
        [HttpPost, Route("Create")]

        public IActionResult Create(Cupom cupom)
        {
            var exists = _cupomService.Get(cupom.Nome);
            if (ModelState.IsValid && !exists.Equals(null))
            {
                _cupomService.Create(cupom);
                return ApiOk("Cupom criado");
            }
            else
            {
                return ApiBadRequest("Erro");
            }
        }
        [HttpPost, Route("Delete")]
        public IActionResult Delete(string nome)
        {
            try
            {
                _cupomService.Delete(nome);
                return ApiOk("Deletado");
            }
            catch
            {
                return ApiBadRequest("Erro");
            }


        }
    }
}
