using AutoMapper;
using Fabrica.Models;
using Fabrica.Web.Models;
using Microsoft.AspNetCore.Identity;

namespace Fabrica.Web.Controllers
{
    using Fabrica.Infrastructure;
    using Fabrica.Services.Contracts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class UsersController : Controller
    {
        private readonly IUsersService usersService;
        private readonly UserManager<FabricaUser> userManager;

        public UsersController(IUsersService usersService, UserManager<FabricaUser> userManager)
        {
            this.usersService = usersService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Profile()
        {
            this.ViewData["CurrentUser"] =  this.usersService.GetUser(this.User.Identity.Name);

            //var currentUserId = this.userManager.GetUserId(this.User);
            //var currentAccount = this.usersService.GetAccountOfTheCurrentUser(currentUserId);

            //var account = Mapper.Map<CreditAccountViewModel>(currentAccount);

            return this.View(/*account*/);
        }

        [Authorize(Roles = GlobalConstants.AdminRoleName)]
        public IActionResult All()
        {
            this.ViewData["AllUsers"] = this.usersService.GetAllUsers();

            return this.View();
        }

    }
}
