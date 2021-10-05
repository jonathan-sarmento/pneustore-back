using Microsoft.AspNetCore.Mvc;

namespace pneustoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CupomController : APIBaseController
    {
        [HttpGet]
        public IActionResult Index() => ApiOk("100% de desconto pra vc");

    }
}