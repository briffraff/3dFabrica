
namespace Fabrica.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}