namespace Fabrica.Web.Controllers
{
    using Fabrica.Infrastructure;
    using Fabrica.Models;
    using Fabrica.Services.Contracts;
    using Fabrica.Services.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using System.Threading.Tasks;

    public class OrdersController : Controller
    {
        private readonly IUsersService usersService;
        private readonly IOrdersService ordersService;
        private readonly IPropsService propsService;
        private readonly ICreditAccountsService accountsService;
        private readonly UserManager<FabricaUser> userManager;

        public OrdersController(IUsersService usersService, IOrdersService ordersService, IPropsService propsService, ICreditAccountsService accountsService, UserManager<FabricaUser> userManager)
        {
            this.ordersService = ordersService;
            this.propsService = propsService;
            this.accountsService = accountsService;
            this.userManager = userManager;
            this.usersService = usersService;
        }

        [Authorize]
        public async Task<IActionResult> AddToBasket(string productId)
        {
            var userId = this.userManager.GetUserId(this.User);
            await this.ordersService.AddToBasket(productId, userId);

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> My()
        {
            var userId = this.userManager.GetUserId(this.User);
            this.ViewData["CurrentUser"] = this.usersService.GetUser(this.User.Identity.Name);

            this.ViewData["props"] = this.ordersService
                .PropsForUser<PropOrderServiceModel>(userId)
                .Result;
                //.Where(x => x.Order.ClientId == userId && x.Order.IsActive)
                //.ToList();

            this.ViewData["marvs"] = this.ordersService
                .MarvsForUser<MarvelousPropOrderServiceModel>(userId)
                .Result;
                //.Where(x => x.Order.ClientId == userId && x.Order.IsActive)
                //.ToList();

            return this.View();
        }

        [Authorize(Roles = GlobalConstants.AdminRoleName)]
        public async Task<IActionResult> All()
        {
            var orders = this.ordersService
                .All<OrderServiceModel>()
                .Result
                .ToList();

            this.ViewData["props"] = this.ordersService
                .propsAll<PropOrderServiceModel>()
                .Result
                .ToList();

            this.ViewData["marvs"] = this.ordersService
                .marvsAll<MarvelousPropOrderServiceModel>()
                .Result
                .ToList();

            return this.View(orders);
        }

        
        [Authorize]
        public async Task<IActionResult> Cancel(string id)
        {
            await this.ordersService.Cancel(id);

            return this.RedirectToAction("My", "Orders");

        }

        [Authorize]
        public async Task<IActionResult> ConfirmAll()
        {
            var userId = this.userManager.GetUserId(this.User);

            await this.ordersService.ConfirmAll(userId);

            return this.RedirectToAction("My", "Orders");
        }

    }
}
