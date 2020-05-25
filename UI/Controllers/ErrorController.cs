using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    [Route("error")]
    public class ErrorController : Controller
    {
        [Route("404")]
        public IActionResult Error404()
        {
            return Content("Url Not Found");
        }

        [Route("500")]
        public IActionResult Error500()
        {
            return Content("Server Error");
        }
    }
}