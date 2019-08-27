namespace Fabrica.Web.Controllers
{
    using Fabrica.Infrastructure;
    using Fabrica.Services.Contracts;
    using Fabrica.Services.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class HomeController : Controller
    {
        private readonly IUsersService usersService;
        private readonly IPropsService propsService;
        private readonly IMarvelousPropsService marvelousService;

        public HomeController(IUsersService usersService, IPropsService propsService, IMarvelousPropsService marvelousService)
        {
            this.usersService = usersService;
            this.propsService = propsService;
            this.marvelousService = marvelousService;
        }
        
        public async Task<IActionResult> Index()
        {
            const bool nonDeletedItems = !GlobalConstants.ShowDeletedItems;

            this.ViewData["CurrentUser"] = this.usersService.GetUser(this.User.Identity.Name);
            this.ViewData["AllProps"] = await this.propsService.GetAll<PropServiceModel>(nonDeletedItems);
            this.ViewData["AllMarvelous"] = await this.marvelousService.GetAll<MarvelousPropServiceModel>(nonDeletedItems);

            return this.View();
        }

    }
}