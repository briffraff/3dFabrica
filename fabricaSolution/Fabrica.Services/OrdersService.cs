namespace Fabrica.Services
{
    using Data;
    using Contracts;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper.QueryableExtensions;
    using Fabrica.Models;
    using Microsoft.EntityFrameworkCore;

    public class OrdersService : DataService,IOrdersService
    {
        public OrdersService(FabricaDBContext context) : base(context)
        {
        }

        //public async Task Create(string propId, string marvPropId, string username)
        //{
        //    var prop = await this.context.Props.FirstOrDefaultAsync(p => p.Id == propId && !p.IsDeleted);

        //    var marvProp = await this.context.MarvelousProps.FirstOrDefaultAsync(mp => mp.Id == marvPropId && !mp.IsDeleted);

        //    var user = await this.context.Users.FirstOrDefaultAsync(u => u.UserName == username);

        //    if (prop == null || marvProp == null ||user == null)
        //    {
        //        return;
        //    }

        //    var order = new Order
        //    {
        //        Client = user,
        //        Prop = prop,
        //        MarvelousProp = marvProp,
        //        OrderedOn = DateTime.Now
        //    };

        //    await this.context.Orders.AddAsync(order);

        //    await this.context.SaveChangesAsync();
        //}

        //public async Task<IEnumerable<OrderServiceModel>> GetAll()
        //{
        //    var orders = await this.context.Orders
        //        .ProjectTo<OrderServiceModel>()
        //        .ToArrayAsync();

        //    return orders;
        //}

    }
}
