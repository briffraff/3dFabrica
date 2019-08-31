namespace Fabrica.Services
{
    using AutoMapper.QueryableExtensions;
    using Contracts;
    using Data;
    using Fabrica.Infrastructure;
    using Fabrica.Infrastructure.Exceptions;
    using Fabrica.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class OrdersService : DataService, IOrdersService
    {

        public OrdersService(FabricaDBContext context) : base(context)
        {

        }

        // ALL ORDERS
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

        //MY ORDERS
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

        // not used method
        public async Task<IEnumerable<T>> My<T>(string userId)
        {
            var client = await this.context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            var orders = this.context.Orders.Where(x => x.Client == client && x.IsActive).ProjectTo<T>();

            return orders;
        }

        //CANCEL ORDER
        public async Task Cancel(string orderId)
        {
            var exceptionMessage = "";

            try
            {
                //var propOrder = await this.context.PropOrders.FirstOrDefaultAsync(x=>x.Order.Id == orderId);
                var order = await this.context.Orders.FirstOrDefaultAsync(x => x.Id == orderId);

                if (order == null)
                {
                    return;
                }

                order.IsActive = false;
                order.IsDeleted = true;

                this.context.Orders.Remove(order);
                await this.context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                exceptionMessage = ex.Message;
                throw new CancelOrderException();
            }
        }

        //2
        // CONFIRM ALL PRODUCTS IN ORDER
        public async Task ConfirmAll(string Id)
        {
            var orders = await this.context.Orders.Where(x => x.ClientId == Id && x.IsActive && !x.IsDeleted).ToArrayAsync();
            var userId = Id;

            foreach (var order in orders)
            {
                string propId = this.context.PropOrders
                    .FirstOrDefault(x => x.OrderId != null && x.OrderId == order.Id)?.PropId;

                string marvId = this.context.MarvelousPropOrders
                    .FirstOrDefault(x => x.OrderId != null && x.OrderId == order.Id)?.MarvelousPropId;

                string productId;

                if (propId != null)
                {
                    productId = propId;
                }
                else
                {
                    productId = marvId;
                }

                if (userId != null)
                {
                    var checkTransaction = "";

                    try
                    {
                        Transaction(productId, userId).Wait();
                    }
                    catch (Exception ex)
                    {
                        checkTransaction = ex.Message.ToLower();
                    }

                    if (checkTransaction.Contains("canceled"))
                    {
                        return;
                    }
                }

                order.IsActive = false;
                order.IsDeleted = true;
            }

            this.context.Orders.UpdateRange(orders);
            await this.context.SaveChangesAsync();
        }

        //3
        // TRANSACTION TO ADMIN,CREATOR AND BUYER
        public async Task Transaction(string productId, string userId)
        {
            var exceptionMessage = "";

            try
            {
                var prop = this.context.Props.FirstOrDefaultAsync(x => x.Id == productId)?.Result;
                var marvProp = this.context.MarvelousProps.FirstOrDefaultAsync(x => x.Id == productId)?.Result;

                var admin = this.context.Users
                    .FirstOrDefaultAsync(x => x.UserName == "bb"
                                              && x.Gender.ToString() == "Male"
                                              && !x.IsDeleted)?.Result;
                CreditAccount adminCreditAccount = null;
                if (admin?.CreditAccountId != null)
                {
                    adminCreditAccount = this.context.CreditAccounts
                        .FirstOrDefaultAsync(a => a.AccountOwnerId == admin.Id)?.Result;
                }

                var client = this.context.Users.FirstOrDefaultAsync(x => x.Id == userId)?.Result;
                CreditAccount account = null;
                if (client?.CreditAccountId != null)
                {
                    account = this.context.CreditAccounts.FirstOrDefaultAsync(a => a.AccountOwnerId == userId)?.Result;
                }

                FabricaUser creator = null;
                if (prop != null)
                {
                    creator = this.context.Users.FirstOrDefaultAsync(x => x.CreatedProps.Contains(prop))?.Result;
                }
                else
                {
                    creator = this.context.Users.FirstOrDefaultAsync(x => x.MarvelousProps.Contains(marvProp))?.Result;
                }

                CreditAccount creatorAccount = null;
                if (creator?.CreditAccountId != null)
                {
                    creatorAccount = this.context.CreditAccounts
                        .FirstOrDefaultAsync(x => x.AccountOwnerId == creator.Id)?.Result;
                }

                var totalCash = GlobalConstants.InitialCash;
                var cashPrice = GlobalConstants.InitialCash;
                var totalPoints = GlobalConstants.InitialPoints;
                var pointsPrice = GlobalConstants.InitialPoints;

                var checkType = "";

                if (marvProp == null && prop == null ||
                    adminCreditAccount == null ||
                    creatorAccount == null ||
                    account == null)
                {
                    var canceledException = new TaskCanceledException();
                    throw canceledException;
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
                        pointsPrice = (int) (pointsPrice * 0.10);
                    }
                }

                if (totalCash > cashPrice || totalPoints > pointsPrice)
                {
                    var PropType = GlobalConstants.PropType;
                    var marvType = GlobalConstants.MarvType;

                    var props = this.context.Props;
                    var marvProps = this.context.MarvelousProps;

                    if (!props.Any() && !marvProps.Any())
                    {
                        return;
                    }

                    if (checkType == PropType)
                    {
                        //CASH
                        //minus cash for buyer
                        account.Cash -= cashPrice;

                        //cash to admin account
                        adminCreditAccount.Cash += (cashPrice * 0.30);

                        //cash to prop creator
                        creatorAccount.Cash += (cashPrice * 0.70);

                        //POINTS
                        //plus points for buyer 
                        var pointsHalf = (int) (cashPrice / GlobalConstants.halfDivider);
                        var bonusPoints = GlobalConstants.InitialPoints;

                        if (cashPrice >= GlobalConstants.quarterDivider)
                        {
                            bonusPoints = (int) (((cashPrice / GlobalConstants.quarterDivider) + GlobalConstants.plus) *
                                                 GlobalConstants.halfPercent);
                        }
                        else
                        {
                            bonusPoints = GlobalConstants.InitialPoints;
                        }

                        account.Points += (pointsHalf + bonusPoints);

                        this.context.CreditAccounts.Update(creatorAccount);
                        this.context.CreditAccounts.Update(adminCreditAccount);
                        this.context.CreditAccounts.Update(account);
                        await this.context.SaveChangesAsync();

                        return;
                    }

                    if (checkType == marvType)
                    {
                        //POINTS
                        //minus points for buyer
                        account.Points -= pointsPrice;

                        //plus points for buyer
                        account.Points += GlobalConstants.winPoints;

                        this.context.CreditAccounts.Update(account);
                        await this.context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                exceptionMessage = ex.Message;
                throw new TransactionException();

            }
        }

        //1
        // ADD PRODUCT TO BASKET
        public async Task AddToBasket(string productId, string userId)
        {
            var exceptionMessage = "";

            try
            {
                var prop = await this.context.Props.FirstOrDefaultAsync(x => x.Id == productId);
                var marvProp = await this.context.MarvelousProps.FirstOrDefaultAsync(x => x.Id == productId);

                var client = await this.context.Users.FirstOrDefaultAsync(x => x.Id == userId);

                var checkType = "";

                if (marvProp == null && prop == null ||
                    client == null)
                {
                    return;
                }

                if (prop != null)
                {
                    checkType = GlobalConstants.PropType;
                }

                if (marvProp != null)
                {
                    checkType = GlobalConstants.MarvType;
                }

                var PropType = GlobalConstants.PropType;
                var marvType = GlobalConstants.MarvType;

                var props = this.context.Props;
                var marvProps = this.context.MarvelousProps;

                if (!props.Any() && !marvProps.Any())
                {
                    return;
                }

                if (checkType == PropType)
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

                    this.context.PropOrders.Add(propOrder);
                    await this.context.SaveChangesAsync();

                    return;
                }

                if (checkType == marvType)
                {
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

                    this.context.MarvelousPropOrders.Add(marvPropOrder);
                    await this.context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                exceptionMessage = ex.Message;
                throw new AddToBasketException();
            }
        }


    }
}

