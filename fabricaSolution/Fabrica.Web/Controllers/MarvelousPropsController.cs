namespace Fabrica.Web.Controllers
{
    using Fabrica.Infrastructure;
    using Fabrica.Models.enums;
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

        public MarvelousPropsController(IMarvelousPropsService marvelousPropsService)
        {
            this.marvelousPropsService = marvelousPropsService;
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
                Type = Enum.Parse<MarvelousType>(model.Type),
                Points = model.Points,
                ImageUrl = model.ImageUrl,
                Hashtags = model.Hashtags,
                Description = model.Description,
            };

            await this.marvelousPropsService.Create(marvelousProp);

            return this.RedirectToAction("Index", "Home");
        }
    }
}
