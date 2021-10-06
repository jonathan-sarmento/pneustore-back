using Microsoft.AspNetCore.Mvc;

namespace pneustoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FreteController : APIBaseController
    {
        /// <summary>
        /// Deve retornar valor de envio para CEPs específicos, por enquanto, não implementado 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Index(/*[FromBody] PostValue postValue*/)
        {
            //var client = new RestClient("https://correios.contrateumdev.com.br/api/cep");
            //client.Timeout = -1;
            //var request = new RestRequest(Method.POST);
            ////var body = @"{ ""cep"" " + ":" + @"""" + postValue.cep + @"""" + "}";
            //string aux1 = "{";
            //string aux2 = @"""cep""";
            //string aux3 = ":";
            //string aux4 = @"""{postValue}""";
            //string aux5 = "}";
            //var body = $"{aux1}{aux2}{aux3}{aux4}{aux5}"
            //request.AddParameter("text/plain", body, ParameterType.RequestBody);
            //IRestResponse response = client.Execute(request);
            //return ApiOk(response);
            return Ok("Frete");
        }

        //public class PostValue
        //{
        //    public string cep { get; set; }
        //}
    }
}