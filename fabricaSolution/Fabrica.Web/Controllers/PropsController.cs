namespace Fabrica.Web.Controllers
{
    using Fabrica.Models;
    using Microsoft.AspNetCore.Identity;
    using Fabrica.Infrastructure;
    using Fabrica.Models.Enums;
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
        private readonly UserManager<FabricaUser> userManager;

        public PropsController(IPropsService propsService,IUsersService usersService,UserManager<FabricaUser> userManager)
        {
            this.propsService = propsService;
            this.usersService = usersService;
            this.userManager = userManager;
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

        [Authorize]
        public async Task<IActionResult> GetProps()
        {
            var userId = this.userManager.GetUserId(this.User);
            var props = await this.propsService.GetUserProps<PropEditViewModel>(userId) ;
            return this.RedirectToAction("Index","Home",props);
        }

        //[Authorize]
        //public IActionResult Details()
        //{
        //    return this.View();
        //}

    }
}
