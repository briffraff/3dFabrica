using System;
using System.Threading.Tasks;
using Fabrica.Infrastructure;
using Fabrica.Models.enums;
using Fabrica.Services.Models;
using Fabrica.Web.Models;
using Microsoft.AspNetCore.Authorization;

namespace Fabrica.Web.Controllers
{
    using Services.Contracts;
    using Microsoft.AspNetCore.Mvc;

    public class PropsController : Controller
    {
        private readonly IPropsService propsService;

        public PropsController(IPropsService propsService)
        {
            this.propsService = propsService;
        }

        [Authorize(Roles = GlobalConstants.UserRoleName)]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.UserRoleName)]
        public async Task<IActionResult> Create(PropEditViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            PropServiceModel prop = new PropServiceModel()
            {
                Name = model.Name,
                Type = Enum.Parse<PropType>(model.Type),
                Price = model.Price,
                ImageUrl = model.ImageUrl,
                Hashtags = model.Hashtags,
                Description = model.Description,
            };

            await this.propsService.Create(prop);

            return this.RedirectToAction("Index", "Home");
        }
    }
}
