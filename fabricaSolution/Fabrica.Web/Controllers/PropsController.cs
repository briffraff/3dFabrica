namespace Fabrica.Web.Controllers
{
    using AutoMapper;
    using Fabrica.Infrastructure;
    using Fabrica.Models;
    using Fabrica.Models.Enums;
    using Fabrica.Services.Models;
    using Fabrica.Web.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Services.Contracts;
    using System;
    using System.Threading.Tasks;

    public class PropsController : Controller
    {
        private readonly IPropsService propsService;
        private readonly IUsersService usersService;
        private readonly UserManager<FabricaUser> userManager;

        public PropsController(IPropsService propsService, IUsersService usersService, UserManager<FabricaUser> userManager)
        {
            this.propsService = propsService;
            this.usersService = usersService;
            this.userManager = userManager;
        }

        //Create
        [Authorize(Roles = GlobalConstants.UserRoleName)]
        public IActionResult Create()
        {
            return this.View();
        }

        //Create Post
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
                PropType = Enum.Parse<PropType>(model.PropType),
                Price = model.Price,
                ImageUrl = model.ImageUrl,
                Hashtags = model.Hashtags,
                Description = model.Description,
                PropCreatorId = this.userManager.GetUserId(this.User),
                //PropCreator = await this.usersService.GetUser(this.User.Identity.Name),
            };

            await this.propsService.Create(prop);

            return this.RedirectToAction("Index", "Home");
        }

        //Edit
        [Authorize(Roles = GlobalConstants.UserRoleName)]
        public async Task<IActionResult> Edit(string id)
        {
            var prop = await this.propsService.GetProp<PropServiceModel>(id);

            if (prop == null)
            {
                return this.RedirectToAction("My", "Props");
            }

            return this.View(prop);
        }

        // Edit
        [HttpPost]
        [Authorize(Roles = GlobalConstants.UserRoleName)]
        public async Task<IActionResult> Edit(PropServiceModel model, string id)
        {
            var serviceModel = Mapper.Map<PropServiceModel>(model);

            serviceModel.Id = id;

            await this.propsService.Edit(serviceModel);

            return this.RedirectToAction("My", "Props", id);
        }

        //Delete
        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            var prop = await this.propsService.GetProp<PropServiceModel>(id);

            if (prop == null)
            {
                return this.RedirectToAction("Index", "Home");
            }

            return this.View(prop);
        }

        // Delete
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(PropEditViewModel model, string id)
        {
            var userId = this.userManager.GetUserId(this.User);
            var user = await this.userManager.FindByIdAsync(userId);

            await this.propsService.Delete(id);

            if (await this.userManager.IsInRoleAsync(user, GlobalConstants.AdminRoleName))
            {
                return this.RedirectToAction("Index", "Home");
            }

            return this.RedirectToAction("My", "Props");
        }

        // RESTORE
        //[HttpPost]
        [Authorize(Roles = GlobalConstants.UserRoleName)]

        public async Task<IActionResult> RestoreProp(string id)
        {
            await this.propsService.Restore(id);

            return this.RedirectToAction("My", "Props");
        }

        //DetailsEdit
        [Authorize]
        [Authorize(Roles = GlobalConstants.UserRoleName)]
        public async Task<IActionResult> DetailsEdit(string id)
        {
            var prop = await this.propsService.GetProp<PropServiceModel>(id);

            return this.View(prop);
        }

        //Restore of deleted props
        [Authorize]
        [Authorize(Roles = GlobalConstants.UserRoleName)]
        public async Task<IActionResult> Restore(string id)
        {
            this.ViewData["CurrentUser"] = this.usersService.GetUser(this.User.Identity.Name);

            var delProp = await this.propsService.GetDelProp(id);

            return this.View(delProp);
        }

        //Details
        [Authorize]
        public async Task<IActionResult> Details(string id)
        {
            var prop = await this.propsService.GetProp<PropServiceModel>(id);

            return this.View(prop);
        }

        //My
        [Authorize(Roles = GlobalConstants.UserRoleName)]
        public async Task<IActionResult> My()
        {
            this.ViewData["CurrentUser"] = this.usersService.GetUser(this.User.Identity.Name);

            var userId = this.userManager.GetUserId(this.User);

            this.ViewData["DeletedProps"] = this.propsService.GetDeletedProps<PropEditViewModel>(userId);

            var props = await this.propsService.GetUserProps<PropEditViewModel>(userId);

            return this.View(props);
        }

        //GetProps
        [Authorize]
        [Authorize(Roles = GlobalConstants.UserRoleName)]
        public async Task<IActionResult> GetProps()
        {
            var userId = this.userManager.GetUserId(this.User);
            var props = await this.propsService.GetUserProps<PropEditViewModel>(userId);
            return this.RedirectToAction("Index", "Home", props);
        }

    }
}