using Microsoft.AspNetCore.Mvc;

namespace pneustoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FreteController : ControllerBase
    {
        /// <summary>
        /// Deve retornar valor de envio para CEPs específicos, por enquanto, não implementado 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Frete gratis");
        }
    }
}