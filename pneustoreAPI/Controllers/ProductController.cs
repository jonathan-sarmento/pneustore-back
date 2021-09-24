using Microsoft.AspNetCore.Mvc;
using pneustoreAPI.Models;
using pneustoreAPI.Services;

namespace pneustoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : APIBaseController
    {
        IService<Product> _service;
        public ProductController(IService<Product> service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Index() => ApiOk(_service.GetAll());

        [Route("{id}"), HttpGet]
        public IActionResult Get(int? id) =>
            _service.Get(id) != null ?
                ApiOk(_service.Get(id)) :
                ApiNotFound($"Produto com id: {id} não existe."); 
        
    }
}
