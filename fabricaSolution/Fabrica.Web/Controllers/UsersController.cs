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
        private readonly SignInManager<FabricaUser> signInManager;

        public UsersController(SignInManager<FabricaUser> signInManager, IUsersService usersService, ICreditAccountsService accountsService, UserManager<FabricaUser> userManager)
        {
            this.signInManager = signInManager;
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

        //Delete profile route
        [Authorize]
        public async Task<IActionResult> Delete()
        {
            var userId = this.userManager.GetUserId(this.User);
            var user = this.userManager.FindByIdAsync(userId);

            return this.PartialView("_DeletePartial", user);
        }
        
        //delete profile
        [Authorize]
        public async Task<IActionResult> DeleteProfile(string id)
        {
            await this.usersService.DeactivateUser(id);
            var check = this.signInManager.SignOutAsync();

            if (check.IsCompleted)
            {
                return this.Redirect("~/Identity/Account/Logout");
            }

            return RedirectToAction("All", "Users");
        }


        //deactivate user
        [Authorize(Roles = GlobalConstants.AdminRoleName)]
        public async Task<IActionResult> Deactivate(string id)
        {
            await this.usersService.DeactivateUser(id);

            //check if logged user is deactivated one then logout
            var userId = this.userManager.GetUserId(this.User);
            var user = await this.userManager.FindByIdAsync(userId);
            var checkRole = await this.userManager.IsInRoleAsync(user, GlobalConstants.AdminRoleName);
            var role = checkRole == true ? GlobalConstants.AdminRoleName : GlobalConstants.UserRoleName;
            
            if (userId == id && role == GlobalConstants.AdminRoleName)
            {
                var check = this.signInManager.SignOutAsync();

                if (check.IsCompleted)
                {
                    return this.Redirect("~/Identity/Account/Logout");
                }
            }

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
