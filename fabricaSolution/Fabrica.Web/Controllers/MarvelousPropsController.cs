using Fabrica.Models;
using Microsoft.AspNetCore.Identity;

namespace Fabrica.Web.Controllers
{
    using Fabrica.Infrastructure;
    using Fabrica.Models.Enums;
    using Fabrica.Services.Contracts;
    using Fabrica.Services.Models;
    using Fabrica.Web.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;

    public class MarvelousPropsController : Controller
    {
        private readonly IMarvelousPropsService marvelousPropsService;
        private readonly IUsersService usersService;
        private readonly UserManager<FabricaUser> userManager;

        public MarvelousPropsController(IMarvelousPropsService marvelousPropsService,
                                        IUsersService usersService,
                                         UserManager<FabricaUser> userManager)
        {
            this.marvelousPropsService = marvelousPropsService;
            this.usersService = usersService;
            this.userManager = userManager;
        }

        [Authorize(Roles = GlobalConstants.AdminRoleName)]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdminRoleName)]
        public async Task<IActionResult> Create(MarvelousPropEditViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            MarvelousPropServiceModel marvelousProp = new MarvelousPropServiceModel()
            {
                Name = model.Name,
                propType = Enum.Parse<MarvelousType>(model.propType),
                Points = model.Points,
                ImageUrl = model.ImageUrl,
                Hashtags = model.Hashtags,
                Description = model.Description,
                MarvelousCreatorId = this.userManager.GetUserId(this.User),
                //MarvelousCreator = await this.usersService.GetUser(this.User.Identity.Name),
            };

            await this.marvelousPropsService.Create(marvelousProp);

            return this.RedirectToAction("Index", "Home");
        }

        //Details
        [Authorize]
        public async Task<IActionResult> Details(string id)
        {
            var prop = await this.marvelousPropsService.GetMarvelousProp<MarvelousPropServiceModel>(id);

            return this.View(prop);
        }
    }
}
