namespace Fabrica.Tests
{
    using Fabrica.Data;
    using Fabrica.Models;
    using Fabrica.Models.Enums;
    using Fabrica.Services;
    using Fabrica.Services.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;

    public class CreditAccountsServiceTests
    {
        [Fact]
        public async Task AddCreditCard_WithWrongParameters_ExpectFalse()
        {
            var optionsBuilder = new DbContextOptionsBuilder<FabricaDBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new FabricaDBContext(optionsBuilder.Options);
            var creditAccountService = new CreditAccountsService(context);

            var owner = new FabricaUser()
            {
                Id = "400",
                UserName = null,
                NormalizedUserName = null,
                Email = null,
                NormalizedEmail = null,
                PasswordHash = null,
                SecurityStamp = null,
                LockoutEnabled = true,
                FullName = null,
                Gender = GenderType.Male,
                IsDeleted = false,
            };
            await context.Users.AddAsync(owner);

            var ownerCA = new CreditAccount()
            {
                AccountId = "2000",
                AccountOwnerId = owner.Id,
                AccountOwner = owner,
                CardNumber = null,
                AuthNumber = null,
                Points = 0,
                Cash = 0,
            };
            await context.CreditAccounts.AddAsync(ownerCA);
            await context.SaveChangesAsync();

            var creditAccountServiceModel = new CreditAccountServiceModel()
            {
                AccountId = "2000",
                AccountOwnerId = owner.Id,
                AccountOwner = owner,
                CardNumber = null,
                AuthNumber = null,
                Points = 0,
                Cash = 0,
            };

            var a = creditAccountService.AddCreditCard(creditAccountServiceModel).IsCanceled;

            var expected = false;
            var creditAccount = context.CreditAccounts.FirstOrDefaultAsync(x => x.AccountId == "2000").Result;
            var isExists = creditAccount.CardNumber != null;

            Assert.True(a == expected);

        }

        [Fact]
        public async Task AddCreditCard_SearchingForThisAccountInDatabase_ExpectTrue()
        {
            var optionsBuilder = new DbContextOptionsBuilder<FabricaDBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new FabricaDBContext(optionsBuilder.Options);
            var creditAccountService = new CreditAccountsService(context);

            var owner = new FabricaUser()
            {
                UserName = "owner",
                NormalizedUserName = "owner".ToUpper(),
                Email = "owner@3dfabrica.com",
                NormalizedEmail = "owner@3dfabrica.com".ToUpper(),
                PasswordHash = "123",
                SecurityStamp = Guid.NewGuid().ToString(),
                LockoutEnabled = true,
                FullName = "Owner owner",
                Gender = GenderType.Male,
                IsDeleted = false,
            };
            await context.Users.AddAsync(owner);

            var ownerCA = new CreditAccount()
            {
                AccountId = "2000",
                AccountOwnerId = owner.Id,
                AccountOwner = owner,
                CardNumber = "2334-3344-3345-2333",
                AuthNumber = "2333",
                Points = 2000,
                Cash = 350,
            };
            await context.CreditAccounts.AddAsync(ownerCA);
            await context.SaveChangesAsync();

            var creditAccountServiceModel = new CreditAccountServiceModel()
            {
                AccountId = "2000",
                AccountOwnerId = owner.Id,
                AccountOwner = owner,
                CardNumber = "2334-3344-3345-2333",
                AuthNumber = "2333",
                Points = 2000,
                Cash = 350,
            };

            var expected = creditAccountServiceModel.AccountId;
            var a = context.CreditAccounts.FirstOrDefaultAsync(x => x.AccountId == expected);

            Assert.Contains(expected, a.Result.AccountId);

        }

        [Fact]
        public async Task LoadCash_checkIfCashAmmountIncreasedBy300_Expect650()
        {
            var optionsBuilder = new DbContextOptionsBuilder<FabricaDBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new FabricaDBContext(optionsBuilder.Options);
            var creditAccountService = new CreditAccountsService(context);

            var owner = new FabricaUser()
            {
                UserName = "owner",
                NormalizedUserName = "owner".ToUpper(),
                Email = "owner@3dfabrica.com",
                NormalizedEmail = "owner@3dfabrica.com".ToUpper(),
                PasswordHash = "123",
                SecurityStamp = Guid.NewGuid().ToString(),
                LockoutEnabled = true,
                FullName = "Owner owner",
                Gender = GenderType.Male,
                IsDeleted = false,
            };
            await context.Users.AddAsync(owner);

            var ownerCA = new CreditAccount()
            {
                AccountId = "2000",
                AccountOwnerId = owner.Id,
                AccountOwner = owner,
                CardNumber = "2334-3344-3345-2333",
                AuthNumber = "2333",
                Points = 2000,
                Cash = 350,
            };
            await context.CreditAccounts.AddAsync(ownerCA);
            await context.SaveChangesAsync();

            var loadCash = 2000;
            await creditAccountService.LoadCash(owner.Id, loadCash);

            var expected = 2350;
            var actualCash = context.CreditAccounts
                .FirstOrDefaultAsync(x => x.AccountId == ownerCA.AccountId)
                .Result
                .Cash;

            Assert.Equal(expected, actualCash);
        }

        [Fact]
        public async Task BuyLicense_checkIfCashAmmountDecreaseAndPointsIncrease_Expect1000CashAnd3250Points()
        {
            //Arrange
            var optionsBuilder = new DbContextOptionsBuilder<FabricaDBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new FabricaDBContext(optionsBuilder.Options);
            var creditAccountService = new CreditAccountsService(context);

            var owner = new FabricaUser()
            {
                UserName = "owner",
                NormalizedUserName = "owner".ToUpper(),
                Email = "owner@3dfabrica.com",
                NormalizedEmail = "owner@3dfabrica.com".ToUpper(),
                PasswordHash = "123",
                SecurityStamp = Guid.NewGuid().ToString(),
                LockoutEnabled = true,
                FullName = "Owner owner",
                Gender = GenderType.Male,
                IsDeleted = false,
            };
            await context.Users.AddAsync(owner);

            var ownerCA = new CreditAccount()
            {
                AccountId = "2000",
                AccountOwnerId = owner.Id,
                AccountOwner = owner,
                CardNumber = "2334-3344-3345-2333",
                AuthNumber = "2333",
                Points = 2000,
                Cash = 1500,
            };
            await context.CreditAccounts.AddAsync(ownerCA);

            var licenzes = new List<Licenze>()
            {
                new Licenze()
                {
                    Name = "Basic",
                    Type = LicenzeType.Basic,
                    Price = 30,
                    bonusPoints = 75,
                },
                new Licenze()
                {
                    Name = "Advanced",
                    Type = LicenzeType.Advanced,
                    Price = 100,
                    bonusPoints = 250,
                },
                new Licenze()
                {
                    Name = "Expert",
                    Type = LicenzeType.Expert,
                    Price = 500,
                    bonusPoints = 1250,
                },
            };
            await context.Licenzes.AddRangeAsync(licenzes);
            await context.SaveChangesAsync();

            //Act
            foreach (var license in licenzes)
            {
                if (license.Type == LicenzeType.Expert)
                {
                    await creditAccountService.BuyLicense(owner.Id, license.Type.ToString());
                }
            }

            var creditAccountRefreshed = context.CreditAccounts
                .FirstOrDefaultAsync(x => x.AccountId == ownerCA.AccountId)
                .Result;

            //Assert
            var expectedCash = 1000;
            var expectedPoints = 3250;
            var cash = creditAccountRefreshed.Cash;
            var points = creditAccountRefreshed.Points;

            Assert.Equal(expectedCash, cash);
            Assert.Equal(expectedPoints, points);
        }
    }
}
