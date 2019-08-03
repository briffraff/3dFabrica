namespace Fabrica.Web.Controllers
{
    using Fabrica.Infrastructure;
    using Fabrica.Models.enums;
    using Fabrica.Services.Models;
    using Fabrica.Web.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services.Contracts;
    using System;
    using System.Threading.Tasks;

    public class PropsController : Controller
    {
        private readonly IPropsService propsService;
        private readonly IUsersService usersService;
        
        public PropsController(IPropsService propsService,IUsersService usersService)
        {
            this.propsService = propsService;
            this.usersService = usersService;
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
                Description = model.Description
            };

            prop.PropCreator = await this.usersService.GetUser(this.User.Identity.Name);


            await this.propsService.Create(prop);

            return this.RedirectToAction("Index", "Home");
        }

    }
}
