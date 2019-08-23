namespace Fabrica.Services
{
    using AutoMapper.QueryableExtensions;
    using Contracts;
    using Data;
    using Fabrica.Infrastructure;
    using Fabrica.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Internal;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class OrdersService : DataService, IOrdersService
    {

        public OrdersService(FabricaDBContext context) : base(context)
        {

        }

        public async Task<IEnumerable<T>> GetAll<T>()
        {
            var orders = await this.context.Orders.ProjectTo<T>().ToArrayAsync();

            return orders;
        }


        public async Task AddToBasket(string productId, string userId)
        {
            var client = await this.context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            var account = await this.context.CreditAccounts.FirstOrDefaultAsync(a => a.AccountOwnerId == userId);
            var prop = await this.context.Props.FirstOrDefaultAsync(x => x.Id == productId);
            var marvProp = await this.context.MarvelousProps.FirstOrDefaultAsync(x => x.Id == productId);

            var totalCash = GlobalConstants.InitialCash;
            var cashPrice = GlobalConstants.InitialCash;
            var totalPoints = GlobalConstants.InitialPoints;
            var pointsPrice = GlobalConstants.InitialPoints;

            if (marvProp == null && prop == null || account == null || client == null)
            {
                return;
            }

            if (prop != null)
            {
                totalCash = account.Cash;
                cashPrice = prop.Price;
            }

            if (marvProp != null)
            {
                totalPoints = account.Points;
                pointsPrice = marvProp.Points;
            }

            if (totalCash > cashPrice || totalPoints > pointsPrice)
            {
                var propType = GlobalConstants.PropType;
                var marvType = GlobalConstants.MarvType;

                var props = this.context.Props;
                var marvProps = this.context.MarvelousProps;

                if (!props.Any() && !marvProps.Any())
                {
                    return;
                }

                var check = props.GetType().ShortDisplayName();

                var checkType = check == propType ? propType : marvType;

                if (checkType == propType)
                {
                    var propOrder = new PropOrder()
                    {
                        Prop = await props.FirstOrDefaultAsync(x => x.Id == productId),
                        PropId = (await props.FirstOrDefaultAsync(x => x.Id == productId)).Id,
                        OrderId = Guid.NewGuid().ToString()
                    };

                    //minus cash
                    account.Cash -= cashPrice;

                    //plus points
                    var pointsHalf = (int)(cashPrice / GlobalConstants.halfDivider);
                    var bonusPoints = GlobalConstants.InitialPoints;

                    if (cashPrice >= GlobalConstants.quarterDivider)
                    {
                        bonusPoints = (int)(((cashPrice / GlobalConstants.quarterDivider) + GlobalConstants.plus) * GlobalConstants.halfPercent);
                    }
                    else
                    {
                        bonusPoints = GlobalConstants.InitialPoints;
                    }

                    account.Points += (pointsHalf + bonusPoints);

                    this.context.CreditAccounts.Update(account);
                    this.context.PropOrders.Update(propOrder);
                    await this.context.SaveChangesAsync();

                    return;
                }

                var marvPropOrder = new MarvelousPropOrder()
                {
                    MarvelousProp = await marvProps.FirstOrDefaultAsync(x => x.Id == productId),
                    MarvelousPropId = (await props.FirstOrDefaultAsync(x => x.Id == productId)).Id,
                    OrderId = Guid.NewGuid().ToString()
                };


                //minus points
                account.Points -= pointsPrice;

                //plus points
                account.Points += GlobalConstants.winPoints;

                this.context.CreditAccounts.Update(account);
                this.context.MarvelousPropOrders.Update(marvPropOrder);
                await this.context.SaveChangesAsync();
            }

        }
    }
}
