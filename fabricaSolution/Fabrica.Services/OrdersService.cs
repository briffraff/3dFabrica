using Fabrica.Services.Models;

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

        public async Task<IEnumerable<T>> propsAll<T>()
        {
            var orders = this.context.PropOrders.ProjectTo<T>();

            return orders;
        }


        public async Task<IEnumerable<T>> marvsAll<T>()
        {
            var orders = this.context.MarvelousPropOrders.ProjectTo<T>();

            return orders;
        }

        public async Task<IEnumerable<T>> All<T>()
        {
            var orders = await this.context.Orders.ProjectTo<T>().ToArrayAsync();

            return orders;
        }

        public async Task<IEnumerable<T>> PropsForUser<T>(string userId)
        {
            var orders = await this.context.PropOrders.Where(x => x.Order.ClientId == userId).ProjectTo<T>().ToArrayAsync();

            return orders;
        }

        public async Task<IEnumerable<T>> MarvsForUser<T>(string userId)
        {
            var orders = await this.context.MarvelousPropOrders.Where(x => x.Order.ClientId == userId).ProjectTo<T>().ToArrayAsync();

            return orders;
        }

        public async Task<IEnumerable<T>> My<T>(string userId)
        {
            var client = await this.context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            var orders = this.context.Orders.Where(x => x.Client == client && x.IsActive).ProjectTo<T>();

            return orders;
        }

        public async Task AddToBasket(string productId, string userId)
        {
            var admin = await this.context.Users
                .FirstOrDefaultAsync(x => x.UserName == "bb"
                                          && x.Gender.ToString() == "Male"
                                          && !x.IsDeleted
                                          && x.CreditAccount.CardNumber != null);

            var adminCreditAccount = await this.context.CreditAccounts.FirstOrDefaultAsync(a => a.AccountOwnerId == admin.Id);

            var client = await this.context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            var account = await this.context.CreditAccounts.FirstOrDefaultAsync(a => a.AccountOwnerId == userId);
            var prop = await this.context.Props.FirstOrDefaultAsync(x => x.Id == productId);
            var marvProp = await this.context.MarvelousProps.FirstOrDefaultAsync(x => x.Id == productId);

            var creator = await this.context.Users.FirstOrDefaultAsync(x => x.CreatedProps.Contains(prop));
            var creatorAccount = await this.context.CreditAccounts.FirstOrDefaultAsync(x => x.AccountOwnerId == creator.Id);

            var totalCash = GlobalConstants.InitialCash;
            var cashPrice = GlobalConstants.InitialCash;
            var totalPoints = GlobalConstants.InitialPoints;
            var pointsPrice = GlobalConstants.InitialPoints;

            var checkType = "";

            if (marvProp == null && prop == null ||
                account == null ||
                client == null ||
                creator == null ||
                creatorAccount == null)
            {
                return;
            }

            if (prop != null)
            {
                checkType = GlobalConstants.PropType;
                totalCash = account.Cash;
                cashPrice = prop.Price;

                //price 90% off for admin
                if (client.Id == admin.Id)
                {
                    cashPrice = (cashPrice * 0.10);
                }
            }

            if (marvProp != null)
            {
                checkType = GlobalConstants.MarvType;
                totalPoints = account.Points;
                pointsPrice = marvProp.Points;

                //points price 90% off for admin
                if (client.Id == admin.Id)
                {
                    pointsPrice = (int)(pointsPrice * 0.10);
                }
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

                if (checkType == propType)
                {
                    var order = new Order()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Client = client,
                        ClientId = client.Id,
                        IsActive = true,
                        IsDeleted = false,
                        OrderedOn = DateTime.Now,
                    };

                    var propOrder = new PropOrder()
                    {
                        Prop = await props.FirstOrDefaultAsync(x => x.Id == productId),
                        PropId = (await props.FirstOrDefaultAsync(x => x.Id == productId)).Id,
                        Order = order,
                        OrderId = order.Id
                    };

                    //CASH
                    //minus cash for buyer
                    account.Cash -= cashPrice;

                    //cash to admin account
                    adminCreditAccount.Cash += (cashPrice * 0.30);

                    //cash to prop creator
                    creatorAccount.Cash += (cashPrice * 0.70);

                    //POINTS
                    //plus points for buyer 
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

                    this.context.CreditAccounts.Update(creatorAccount);
                    this.context.CreditAccounts.Update(adminCreditAccount);
                    this.context.CreditAccounts.Update(account);
                    this.context.PropOrders.Add(propOrder);
                    await this.context.SaveChangesAsync();

                    return;
                }

                var marvOrder = new Order()
                {
                    Id = Guid.NewGuid().ToString(),
                    Client = client,
                    ClientId = client.Id,
                    IsActive = true,
                    IsDeleted = false,
                    OrderedOn = DateTime.Now,
                };

                var marvPropOrder = new MarvelousPropOrder()
                {
                    MarvelousProp = await marvProps.FirstOrDefaultAsync(x => x.Id == productId),
                    MarvelousPropId = (await marvProps.FirstOrDefaultAsync(x => x.Id == productId)).Id,
                    Order = marvOrder,
                    OrderId = marvOrder.Id,
                };

                //POINTS
                //minus points for buyer
                account.Points -= pointsPrice;

                //plus points for buyer
                account.Points += GlobalConstants.winPoints;

                this.context.CreditAccounts.Update(account);
                this.context.MarvelousPropOrders.Add(marvPropOrder);
                await this.context.SaveChangesAsync();
            }

        }
    }
}
