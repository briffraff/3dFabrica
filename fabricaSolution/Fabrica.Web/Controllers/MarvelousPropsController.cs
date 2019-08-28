namespace Fabrica.Web.Controllers
{
    using Fabrica.Infrastructure;
    using Fabrica.Models;
    using Fabrica.Models.Enums;
    using Fabrica.Services.Contracts;
    using Fabrica.Services.Models;
    using Fabrica.Web.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
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
                PropType = Enum.Parse<MarvelousType>(model.PropType),
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

        //Delete
        [Authorize(Roles = GlobalConstants.AdminRoleName)]
        public async Task<IActionResult> Delete(string id)
        {
            var prop = await this.marvelousPropsService.GetMarvelousProp<MarvelousPropServiceModel>(id);

            if (prop == null)
            {
                return this.RedirectToAction("Index", "Home");
            }

            return this.View(prop);
        }

        // Delete
        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdminRoleName)]
        public async Task<IActionResult> Delete(MarvelousPropEditViewModel model, string id)
        {
            var userId = this.userManager.GetUserId(this.User);
            var user = await this.userManager.FindByIdAsync(userId);

            await this.marvelousPropsService.Delete(id);

            if (await this.userManager.IsInRoleAsync(user, GlobalConstants.AdminRoleName))
            {
                return this.RedirectToAction("Index", "Home");
            }

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
