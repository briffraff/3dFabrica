namespace Fabrica.Web.Controllers
{
    using Fabrica.Infrastructure;
    using Fabrica.Models;
    using Fabrica.Services.Contracts;
    using Fabrica.Services.Models;
    using Fabrica.Web.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class CreditAccountsController : Controller
    {
        private readonly ICreditAccountsService accountService;
        private readonly IUsersService usersService;
        private readonly UserManager<FabricaUser> userManager;

        public CreditAccountsController(ICreditAccountsService accountService, IUsersService usersService, UserManager<FabricaUser> userManager)
        {
            this.accountService = accountService;
            this.usersService = usersService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult AddCreditCard()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddCreditCard(AddCreditCardViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            CreditAccountServiceModel creditCard = new CreditAccountServiceModel()
            {
                CardNumber = model.CardNumber,
                Cash = model.Cash,
                Points = GlobalConstants.firstBonusPointsWhenRegisterAccount,
                AccountOwnerId = this.userManager.GetUserId(this.User)
            };

            //transfer account
            await this.accountService.AddCreditCard(creditCard);

            //get info for user
            var currentUser = this.userManager.GetUserName(this.User);
            var currentUserId = this.userManager.GetUserId(this.User);

            var user = this.usersService.GetUser(currentUser).Result;

            //transfer credit account id to user
            await this.usersService.GetAccountIdAndSetToUser(currentUser, currentUserId);

            return this.RedirectToAction("Profile", "Users");
        }


        [Authorize]
        public IActionResult LoadCash()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> LoadCash(LoadCashViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            CreditAccountServiceModel creditCard = new CreditAccountServiceModel()
            {
                Cash = model.Cash,
            };

            var currentUserId = this.userManager.GetUserId(this.User);
            var cashToLoad = creditCard.Cash;

            await accountService.LoadCash(currentUserId,cashToLoad);
            
            return this.RedirectToAction("Profile", "Users");
        }

        [Authorize]
        public IActionResult BuyLicense()
        {
            return this.View();
        }



    }
}
