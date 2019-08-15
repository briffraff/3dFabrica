namespace Fabrica.Web.Controllers
{
    using AutoMapper;
    using Fabrica.Infrastructure;
    using Fabrica.Models;
    using Fabrica.Services.Contracts;
    using Fabrica.Web.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using System.Threading.Tasks;

    public class UsersController : Controller
    {
        private readonly IUsersService usersService;
        private readonly ICreditAccountsService accountsService;
        private readonly UserManager<FabricaUser> userManager;

        public UsersController(IUsersService usersService, ICreditAccountsService accountsService, UserManager<FabricaUser> userManager)
        {
            this.usersService = usersService;
            this.accountsService = accountsService;
            this.userManager = userManager;
        }

        //Profile
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            this.ViewData["CurrentUser"] = this.usersService.GetUser(this.User.Identity.Name);

            var currentUserId = this.userManager.GetUserId(this.User);
            var currentAccount = await this.accountsService.GetCurrentAccount<CreditAccountViewModel>(currentUserId);

            return this.View(currentAccount);
        }

        //All users
        [Authorize(Roles = GlobalConstants.AdminRoleName)]
        public IActionResult All()
        {
            var allUsers = this.usersService.GetAllUsers()
                .Result
                .Select(Mapper.Map<AllUsersViewModel>)
                .ToArray();

            return this.View(allUsers);
        }

        //Delete user
        [Authorize(Roles = GlobalConstants.AdminRoleName)]
        public async Task<IActionResult> Deactivate(string id)
        {
            //var user = await userManager.FindByIdAsync(id);
            var user = await this.usersService.Get(id);

            //await userManager.DeleteAsync(user);
            await this.usersService.DeactivateUser(id);

            return RedirectToAction("All", "Users");

        }

        //Activate user
        [Authorize(Roles = GlobalConstants.AdminRoleName)]
        public async Task<IActionResult> Activate(string id)
        {
            await this.usersService.ActivateUser(id);

            return this.RedirectToAction("All", "Users");
        }

    }
}
