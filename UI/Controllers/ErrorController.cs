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

        [Route("script")]
        public IActionResult ScriptError()
        {
            return Content("Your Url Have ScriptData");
        }

        [Route("500")]
        public IActionResult Error500()
        {
            return Content("Server Error");
        }

        public IActionResult NotValid(params string[] errors)
        {
            return Content("not valid input");
        }
    }
}