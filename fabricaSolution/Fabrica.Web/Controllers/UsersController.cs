using System.Threading.Tasks;
using Fabrica.Web.Models;

namespace Fabrica.Web.Controllers
{
    using Fabrica.Infrastructure;
    using Fabrica.Models;
    using Fabrica.Services.Contracts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class UsersController : Controller
    {
        private readonly IUsersService usersService;
        private readonly ICreditAccountsService accountsService;
        private readonly UserManager<FabricaUser> userManager;

        public UsersController(IUsersService usersService,ICreditAccountsService accountsService, UserManager<FabricaUser> userManager)
        {
            this.usersService = usersService;
            this.accountsService = accountsService;
            this.userManager = userManager;
        }

        //Profile
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            this.ViewData["CurrentUser"] =  this.usersService.GetUser(this.User.Identity.Name);

            var currentUserId = this.userManager.GetUserId(this.User);
            var currentAccount = await this.accountsService.GetCurrentAccount<CreditAccountViewModel>(currentUserId);

            return this.View(currentAccount);
        }

        //All users
        [Authorize(Roles = GlobalConstants.AdminRoleName)]
        public IActionResult All()
        {
            this.ViewData["AllUsers"] = this.usersService.GetAllUsers();

            return this.View();
        }

    }
}
