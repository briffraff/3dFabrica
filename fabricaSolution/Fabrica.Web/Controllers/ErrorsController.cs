namespace Fabrica.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class ErrorsController : Controller
    {
        [Route("Error/404")]
        public async Task<IActionResult> PageNotFound()
        {
            return this.View();
        }

        [Route("Error/{code:int}")]
        public async Task<IActionResult> Error(int code)
        {
            return View();
        }
    }
}
