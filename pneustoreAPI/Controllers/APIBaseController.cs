using Microsoft.AspNetCore.Mvc;
using pneustoreAPI.API;

namespace pneustoreAPI.Controllers
{
    public abstract class APIBaseController : ControllerBase
    {
        protected OkObjectResult ApiOk<T>(T Results) =>
            Ok(CustomResponse("" ,true , Results));
        protected OkObjectResult ApiOk(string Message = "") =>
            Ok(CustomResponse(Message ,true));
        protected CreatedResult ApiCreated(string Message = "") =>
            Created("", CustomResponse(Message ,true));
        protected NotFoundObjectResult ApiNotFound(string Message = "") =>
            NotFound(CustomResponse(Message ,false));
        protected BadRequestObjectResult ApiBadRequest<T>(T results, string Message = "") => 
            BadRequest(CustomResponse(Message, false, results));


        #region Metodos privados
        APIResponse<T> CustomResponse<T>(string message, bool succeed, T results) =>
            new APIResponse<T>()
            {
                Message = message,
                Succeed = succeed,
                Results = results
            };

        APIResponse<string> CustomResponse(string message, bool succeed) =>
            new APIResponse<string>()
            {
                Message = message,
                Succeed = succeed,
            };

        #endregion
    }
}