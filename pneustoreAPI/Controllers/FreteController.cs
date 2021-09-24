using Microsoft.AspNetCore.Mvc;

namespace pneustoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FreteController : ControllerBase
    {
        /// <summary>
        /// Deve retornar valor de envio para CEPs espec�ficos, por enquanto, n�o implementado 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Frete gratis");
        }
    }
}