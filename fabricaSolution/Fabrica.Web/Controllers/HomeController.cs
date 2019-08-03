namespace Fabrica.Web.Controllers
{
    using Fabrica.Services.Contracts;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class HomeController : Controller
    {
        private readonly IUsersService usersService;

        public HomeController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public async Task<IActionResult> Index()
        {
            this.ViewData["CurrentUser"] = this.usersService.GetUser(this.User.Identity.Name);
            return this.View();
        }

    }
}