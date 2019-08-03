namespace Fabrica.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Fabrica.Services.Contracts;
    using Microsoft.AspNetCore.Authorization;
    using AutoMapper;
    using Fabrica.Infrastructure;
    using Fabrica.Web.Models;

    public class OrdersController : Controller
    {
        //private readonly IOrdersService ordersService;

        //public OrdersController(IOrdersService ordersService)
        //{
        //    this.ordersService = ordersService;
        //}

        //[Authorize]
        //public async Task<IActionResult> Create(string propId,string marvPropId)
        //{
        //    await this.ordersService.Create(propId, marvPropId, this.User.Identity.Name);

        //    return this.RedirectToAction("Index", "Home");
        //}

        //[Authorize(Roles = GlobalConstants.UserRoleName)]
        public IActionResult My()
        {
            return this.View();
        }

        //[Authorize(Roles = GlobalConstants.AdminRoleName)]
        //public async Task<IActionResult> All()
        //{
        //    var orders = (await this.ordersService.GetAll())
        //        .Select(Mapper.Map<OrderListViewModel>)
        //        .ToArray();

        //    return this.View(orders);
        //}

    }
}
