using Microsoft.AspNetCore.Mvc;

namespace BookWorld.Web.Controllers
{
    public class ErrorPagesController : Controller
    {
        [Route("Unauthorized")]
        public IActionResult _Unauthorized()
        {
            return View();
        }

        [Route("BadRequest")]
        public IActionResult _BadRequest()
        {
            return View();
        }
    }
}
