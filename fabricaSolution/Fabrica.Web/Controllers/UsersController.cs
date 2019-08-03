namespace Fabrica.Web.Controllers
{
    using Fabrica.Infrastructure;
    using Fabrica.Services.Contracts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class UsersController : Controller
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [Authorize]
        public IActionResult Profile()
        {
           this.ViewData["CurrentUser"] =  this.usersService.GetUser(this.User.Identity.Name);
            return this.View();
        }

        [Authorize(Roles = GlobalConstants.AdminRoleName)]
        public IActionResult All()
        {
            this.ViewData["AllUsers"] = this.usersService.GetAllUsers();

            return this.View();
        }


    }
}
