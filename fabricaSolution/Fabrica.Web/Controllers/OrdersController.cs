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
        private readonly IOrdersService ordersService;
        private readonly IPropsService propsService;
        private readonly ICreditAccountsService accountsService;
        private readonly UserManager<FabricaUser> userManager;

        public OrdersController(IOrdersService ordersService, IPropsService propsService, ICreditAccountsService accountsService, UserManager<FabricaUser> userManager)
        {
            this.ordersService = ordersService;
            this.propsService = propsService;
            this.accountsService = accountsService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> AddToBasket(string productId)
        {
            var userId = this.userManager.GetUserId(this.User);
            await this.ordersService.AddToBasket(productId, userId);

            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = GlobalConstants.UserRoleName)]
        public async Task<IActionResult> My()
        {
            var userId = this.userManager.GetUserId(this.User);

            var orders = (await this.ordersService
                .GetAll<OrderServiceModel>())
                .Where(x => x.ClientId == userId);

            return this.View(orders);
        }

        [Authorize(Roles = GlobalConstants.AdminRoleName)]
        public async Task<IActionResult> All()
        {
            var orders = await this.ordersService.GetAll<OrderServiceModel>();

            return this.View(orders);
        }

    }
}
