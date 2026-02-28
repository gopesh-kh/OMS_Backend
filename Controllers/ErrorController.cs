using Microsoft.AspNetCore.Mvc;

namespace OMS_Backend.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("/error")]
        public IActionResult ErrorHandler()
        {
            return Problem(
                title: "unexpected error occured",
                statusCode: 500
            );
        }
    }
}
