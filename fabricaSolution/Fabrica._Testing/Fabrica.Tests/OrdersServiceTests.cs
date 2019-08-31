namespace Fabrica.Tests
{
    using Fabrica.Data;
    using Fabrica.Infrastructure;
    using Fabrica.Models;
    using Fabrica.Models.Enums;
    using Fabrica.Services;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class OrdersServiceTests
    {
        [Fact]
        public async Task AllOrders_GetAllOrders_Expected2andTrue()
        {
            var optionsBuilder = new DbContextOptionsBuilder<FabricaDBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new FabricaDBContext(optionsBuilder.Options);
            var ordersService = new OrdersService(context);

            var client = new FabricaUser
            {
                Id = "5000",
                UserName = "bb",
                NormalizedUserName = "bb".ToUpper(),
                Email = "bb@3dfabrica.com",
                NormalizedEmail = "bb@3dfabrica.com".ToUpper(),
                PasswordHash = "123",
                SecurityStamp = Guid.NewGuid().ToString(),
                LockoutEnabled = true,
                FullName = "Boraka",
                Gender = GenderType.Male,
                IsDeleted = false,
            };
            await context.Users.AddAsync(client);

            var props = new List<Prop>()
            {
                new Prop()
                {
                    Name = "LS Crewneck",
                    Description = "Long Sleeve Pullover. 90% Cotton, 10% Silk",
                    PropType = PropType.LS,
                    Hashtags = "#LS #Pullover #Cotton",
                    ImageUrl = GlobalConstants.LSCrewneck,
                    Price = 56.5,
                    PropCreator = client
                },
                new Prop()
                {
                    Name = "SS Crewneck",
                    Description = "Short Sleeve Shirt. 50% Cotton, 50% Silk",
                    PropType = PropType.SS,
                    Hashtags = "#SS #T-shirt #50/50",
                    ImageUrl = GlobalConstants.SSCrewneck,
                    Price = 60,
                    PropCreator = client
                }
            };

            await context.Props.AddRangeAsync(props);
            await context.SaveChangesAsync();

            foreach (var prop in props)
            {
                await ordersService.AddToBasket(prop.Id, client.Id);
            }

            var actualCountAll = context.Orders.Count();

            var expected = 2;
            Assert.True(actualCountAll == expected);
        }

        [Fact]
        public async Task AddToBasket_CountHowMuchOrdersCreated_Expected1()
        {
            var optionsBuilder = new DbContextOptionsBuilder<FabricaDBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new FabricaDBContext(optionsBuilder.Options);
            var ordersService = new OrdersService(context);

            var client = new FabricaUser
            {
                Id = "5000",
                UserName = "bb",
                NormalizedUserName = "bb".ToUpper(),
                Email = "bb@3dfabrica.com",
                NormalizedEmail = "bb@3dfabrica.com".ToUpper(),
                PasswordHash = "123",
                SecurityStamp = Guid.NewGuid().ToString(),
                LockoutEnabled = true,
                FullName = "Boraka",
                Gender = GenderType.Male,
                IsDeleted = false,
            };
            await context.Users.AddAsync(client);

            var prop = new Prop()
            {
                Name = "LS Crewneck",
                Description = "Long Sleeve Pullover. 90% Cotton, 10% Silk",
                PropType = PropType.LS,
                Hashtags = "#LS #Pullover #Cotton",
                ImageUrl = GlobalConstants.LSCrewneck,
                Price = 56.5,
                PropCreator = client
            };
            await context.Props.AddAsync(prop);
            await context.SaveChangesAsync();

            await ordersService.AddToBasket(prop.Id, client.Id);

            var actualCountAll = context.Orders.Count();

            var expected = 1;
            Assert.Equal(expected, actualCountAll);
        }

        [Fact]
        public async Task AddToBasket_CountPropsButWithWrongInput_Expected0()
        {
            var optionsBuilder = new DbContextOptionsBuilder<FabricaDBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new FabricaDBContext(optionsBuilder.Options);
            var ordersService = new OrdersService(context);

            var client = new FabricaUser
            {
                Id = null,
                UserName = null,
                NormalizedUserName = "bb".ToUpper(),
                Email = null,
                NormalizedEmail = null,
                PasswordHash = null,
                SecurityStamp = null,
                LockoutEnabled = true,
                FullName = null,
                IsDeleted = false,
            };
            await context.Users.AddAsync(client);

            var prop = new Prop()
            {
                Name = null,
                Description = null,
                PropType = PropType.Bras,
                Hashtags = null,
            };
            await context.Props.AddAsync(prop);

            await ordersService.AddToBasket(prop.Id, client.Id);

            var actualCountAll = context.Orders.Count();

            var expected = 0;
            Assert.Equal(expected, actualCountAll);
        }

        [Fact]
        public async Task ConfirmAll_CountConfirmedOrders_Expected1()
        {
            var optionsBuilder = new DbContextOptionsBuilder<FabricaDBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new FabricaDBContext(optionsBuilder.Options);
            var ordersService = new OrdersService(context);

            var client = new FabricaUser
            {
                Id = "5000",
                UserName = "bb",
                NormalizedUserName = "bb".ToUpper(),
                Email = "bb@3dfabrica.com",
                NormalizedEmail = "bb@3dfabrica.com".ToUpper(),
                PasswordHash = "123",
                SecurityStamp = Guid.NewGuid().ToString(),
                LockoutEnabled = true,
                FullName = "Boraka",
                Gender = GenderType.Male,
                IsDeleted = false,
            };
            await context.Users.AddAsync(client);

            var props = new List<Prop>()
            {
                new Prop()
                {
                    Name = "LS Crewneck",
                    Description = "Long Sleeve Pullover. 90% Cotton, 10% Silk",
                    PropType = PropType.LS,
                    Hashtags = "#LS #Pullover #Cotton",
                    ImageUrl = GlobalConstants.LSCrewneck,
                    Price = 56.5,
                    PropCreator = client
                },
                new Prop()
                {
                    Name = "SS Crewneck",
                    Description = "Short Sleeve Shirt. 50% Cotton, 50% Silk",
                    PropType = PropType.SS,
                    Hashtags = "#SS #T-shirt #50/50",
                    ImageUrl = GlobalConstants.SSCrewneck,
                    Price = 60,
                    PropCreator = client
                }
            };

            await context.Props.AddRangeAsync(props);
            await context.SaveChangesAsync();

            foreach (var prop in props)
            {
                await ordersService.AddToBasket(prop.Id, client.Id);
            }

            await ordersService.ConfirmAll(client.Id);

            var actualCountAll = context.Orders.Count();

            var expected = 2;
            Assert.Equal(expected, actualCountAll);
        }

        [Fact]
        public async Task Cancel_checkIfCancelMethodWorks_ExpectedTrue()
        {
            var optionsBuilder = new DbContextOptionsBuilder<FabricaDBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new FabricaDBContext(optionsBuilder.Options);
            var ordersService = new OrdersService(context);

            var client = new FabricaUser
            {
                Id = "5000",
                UserName = "bb",
                NormalizedUserName = "bb".ToUpper(),
                Email = "bb@3dfabrica.com",
                NormalizedEmail = "bb@3dfabrica.com".ToUpper(),
                PasswordHash = "123",
                SecurityStamp = Guid.NewGuid().ToString(),
                LockoutEnabled = true,
                FullName = "Boraka",
                Gender = GenderType.Male,
                IsDeleted = false,
            };
            await context.Users.AddAsync(client);

            var prop = new Prop()
            {
                Name = "LS Crewneck",
                Description = "Long Sleeve Pullover. 90% Cotton, 10% Silk",
                PropType = PropType.LS,
                Hashtags = "#LS #Pullover #Cotton",
                ImageUrl = GlobalConstants.LSCrewneck,
                Price = 56.5,
                PropCreator = client
            };
            await context.Props.AddAsync(prop);

            var order = new Order()
            {
                Id = "1000",
                Client = client,
                ClientId = client.Id,
                IsActive = true,
                IsDeleted = false,
            };
            await context.Orders.AddAsync(order);

            await context.SaveChangesAsync();

            await ordersService.Cancel(order.Id);

            var actualCountAll = context.Orders.Count();

            var expected = 0;
            Assert.Equal(expected, actualCountAll);
        }
    }
}
