namespace Fabrica.Web.Controllers
{
    using Fabrica.Models;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
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

        //Edit
        [Authorize(Roles = GlobalConstants.UserRoleName)]
        public async Task<IActionResult> Edit(string id)
        {
            this.ViewData["CurrentUser"] = this.usersService.GetUser(this.User.Identity.Name);

            var prop = await this.propsService.GetProp(id);

            if (prop == null)
            {
                return this.RedirectToAction("My", "Props");
            }

            //var viewModel = Mapper.Map<PropEditViewModel>(prop);

            return this.View(prop);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.UserRoleName)]
        public async Task<IActionResult> Edit(Prop model, string id)
        {
            this.ViewData["CurrentUser"] = this.usersService.GetUser(this.User.Identity.Name);

            var serviceModel = Mapper.Map<PropServiceModel>(model);

            serviceModel.Id = id;

            await this.propsService.Edit(serviceModel);

            return this.RedirectToAction("My", "Props", id);
        }

        //Delete
        [Authorize]
        [Authorize(Roles = GlobalConstants.UserRoleName)]
        public async Task<IActionResult> Delete(string id)
        {
            this.ViewData["CurrentUser"] = this.usersService.GetUser(this.User.Identity.Name);

            var prop = await this.propsService.GetProp(id);

            if (prop == null)
            {
                return this.RedirectToAction("Index", "Home");
            }

            //var viewModel = Mapper.Map<PropEditViewModel>(prop);

            return this.View(prop);
        }

        // DELETE
        [HttpPost]
        [Authorize]
        [Authorize(Roles = GlobalConstants.UserRoleName)]
        public async Task<IActionResult> Delete(PropEditViewModel model, string id)
        {
            await this.propsService.Delete(id);

            return this.RedirectToAction("My", "Props");
        }


        // RESTORE
        //[HttpPost]
        [Authorize]
        [Authorize(Roles = GlobalConstants.UserRoleName)]

        public async Task<IActionResult> Restore(string id)
        {
            await this.propsService.Activate(id);

            return this.RedirectToAction("My", "Props");
        }

        //DetailsEdit
        [Authorize]
        [Authorize(Roles = GlobalConstants.UserRoleName)]
        public async Task<IActionResult> DetailsEdit(string id)
        {
            this.ViewData["CurrentUser"] = this.usersService.GetUser(this.User.Identity.Name);

            var prop = await this.propsService.GetProp(id);

            return this.View(prop);
        }

        //DetailsEdit of deleted props
        [Authorize]
        [Authorize(Roles = GlobalConstants.UserRoleName)]
        public async Task<IActionResult> DeletedDetailsEdit(string id)
        {
            this.ViewData["CurrentUser"] = this.usersService.GetUser(this.User.Identity.Name);

            var delProp = await this.propsService.GetDelProp(id);

            return this.View(delProp);
        }

        //Details
        [Authorize]
        public IActionResult Details()
        {
            return this.View();
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