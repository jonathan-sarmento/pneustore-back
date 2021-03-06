using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pneustoreAPI.API;
using pneustoreAPI.Models;
using pneustoreAPI.Services;

namespace pneustoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class ProductController : APIBaseController
    {
        private readonly IService<Product> _service;
        public ProductController(IService<Product> service)
        {
            _service = service;
        }
        /// <summary>
        /// Retorna lista de produtos (pneus) cadastrados no banco de dados
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public IActionResult Index() 
            => ApiOk(_service.GetAll());
            

        /// <summary>
        /// Retorna produto especifíco cadastrado no banco 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}"), HttpGet]
        public IActionResult Get(int? id) 
            => _service.Get(id) != null ?
                    ApiOk(_service.Get(id)) :
                    ApiNotFound($"Produto com id: {id} não existe."); 
        
    }
}
