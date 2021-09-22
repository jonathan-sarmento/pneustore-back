using Microsoft.AspNetCore.Mvc;

namespace pneustoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FreteController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Frete gratis");
        }
    }
}