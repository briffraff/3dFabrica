namespace Fabrica.Data.Seeds
{
    using Fabrica.Infrastructure;
    using Fabrica.Models;
    using Fabrica.Models.Enums;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;

    public class FabricaDbSeedData
    {
        private readonly FabricaDBContext context;
        private readonly IApplicationBuilder app;
        private readonly IHostingEnvironment env;
        private readonly UserManager<FabricaUser> userManager;

        public FabricaDbSeedData(FabricaDBContext context,
            IApplicationBuilder app,
            IHostingEnvironment env,
            UserManager<FabricaUser> userManager)
        {
            this.context = context;
            this.app = app;
            this.env = env;
            this.userManager = userManager;
        }

        //  database
        private void SeedDatabase()
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<FabricaDBContext>();

                if (env.IsDevelopment())
                {
                    dbContext.Database.Migrate();
                }
            }
        }

        //  roles
        private async void SeedRoles()
        {
            if (this.context.Roles.Any()) return;

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                if (!roleManager.RoleExistsAsync(GlobalConstants.AdminRoleName).Result)
                {
                    await roleManager.CreateAsync(new IdentityRole(GlobalConstants.AdminRoleName));
                }

                if (!roleManager.RoleExistsAsync(GlobalConstants.UserRoleName).Result)
                {
                    await roleManager.CreateAsync(new IdentityRole(GlobalConstants.UserRoleName));
                }
            }
        }

        //  admins
        private void SeedAdmins()
        {
            var adminRole = GlobalConstants.AdminRoleName;

            var usersFromDb = this.context.Users;

            var admins = new List<FabricaUser>()
            {
                new FabricaUser
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
                },
                new FabricaUser
                {
                    UserName = "admincheto",
                    NormalizedUserName = "admincheto".ToUpper(),
                    Email = "admin@3dfabrica.com",
                    NormalizedEmail = "admin@3dfabrica.com".ToUpper(),
                    PasswordHash = "123",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    LockoutEnabled = true,
                    FullName = "Admin Adminchev",
                    Gender = GenderType.Male,
                    IsDeleted = false,
                },
                new FabricaUser
                {
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
                }
            };

            HashPaswword(admins, usersFromDb);
            CreateUserAddRole(admins, usersFromDb, adminRole).Wait();
            this.context.SaveChanges();

        }



        //  users
        public void SeedUsers()
        {
            var userRole = GlobalConstants.UserRoleName;

            var usersFromDb = this.context.Users;

            var usersToDb = new List<FabricaUser>()
            {
                new FabricaUser
                {
                    UserName = "aa",
                    NormalizedUserName = "aa".ToUpper(),
                    Email = "aa@3dfabrica.com",
                    NormalizedEmail = "aa@3dfabrica.com".ToUpper(),
                    PasswordHash = "123",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    LockoutEnabled = true,
                    FullName = "a a",
                    Gender = GenderType.Female,
                    IsDeleted = false
                },
                new FabricaUser
                {
                    UserName = "sashoto",
                    NormalizedUserName = "sashoto".ToUpper(),
                    Email = "sashoto@3dfabrica.com",
                    NormalizedEmail = "sashoto@3dfabrica.com".ToUpper(),
                    PasswordHash = "123",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    LockoutEnabled = true,
                    FullName = "Sashoto Sashev",
                    Gender = GenderType.Male,
                    IsDeleted = false
                },
                new FabricaUser
                {
                    UserName = "stavreto",
                    NormalizedUserName = "stavreto".ToUpper(),
                    Email = "stavreto@3dfabrica.com",
                    NormalizedEmail = "stavreto@3dfabrica.com".ToUpper(),
                    PasswordHash = "123",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    LockoutEnabled = true,
                    FullName = "Stavreto Stavrito",
                    Gender = GenderType.Male,
                    IsDeleted = false
                },
            };

            HashPaswword(usersToDb, usersFromDb);
            CreateUserAddRole(usersToDb, usersFromDb, userRole).Wait();
            this.context.SaveChanges();
        }

        //Credit Accounts
        private void SeedAccounts()
        {
            if (this.context.CreditAccounts.Any()) return;

            var owner = this.context.Users.FirstOrDefault(x => x.UserName == "owner");
            var mainAdmin = this.context.Users.FirstOrDefault(x => x.UserName == "bb");
            var aa = this.context.Users.FirstOrDefault(x => x.UserName == "aa");

            var ownerCA = new CreditAccount()
            {
                AccountId = Guid.NewGuid().ToString(),
                AccountOwnerId = owner.Id,
                AccountOwner = owner,
                CardNumber = "2334-3344-3345-2333",
                AuthNumber = "2333",
                Points = 2000,
                Cash = 350,
            };

            var adminCA = new CreditAccount()
            {
                AccountId = Guid.NewGuid().ToString(),
                AccountOwnerId = mainAdmin.Id,
                AccountOwner = mainAdmin,
                CardNumber = "4444-5555-6666-7777",
                AuthNumber = "7777",
                Cash = 450,
                Points = 3500,
            };

            var aaCA = new CreditAccount()
            {
                AccountId = Guid.NewGuid().ToString(),
                AccountOwnerId = aa.Id,
                AccountOwner = aa,
                CardNumber = "2269-6969-6969-4525",
                AuthNumber = "4525",
                Cash = 500,
                Points = 3000,
            };

            ownerCA.CardNumber = HashCreditCardNumber(ownerCA.CardNumber).Result;
            adminCA.CardNumber = HashCreditCardNumber(adminCA.CardNumber).Result;
            aaCA.CardNumber = HashCreditCardNumber(aaCA.CardNumber).Result;

            this.context.CreditAccounts.Add(ownerCA);
            this.context.CreditAccounts.Add(adminCA);
            this.context.CreditAccounts.Add(aaCA);
            this.context.SaveChanges();
        }


        public async Task<string> HashCreditCardNumber(string cardNumber)
        {
            using (SHA512 sha512Hash = SHA512.Create())
            {
                var hashedCreditCardNumber =
                    Encoding.UTF8.GetString(sha512Hash.ComputeHash(Encoding.UTF8.GetBytes(cardNumber)));

                return hashedCreditCardNumber;
            }
        }

        private void SeedLicenzes()
        {
            if (this.context.Licenzes.Any()) return;

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

            this.context.Licenzes.AddRange(licenzes);
            this.context.SaveChanges();
        }

        //  Props
        private void SeedProps()
        {
            if (this.context.Props.Any()) return;

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
                    PropCreator = this.context.Users.FirstOrDefault(u => u.UserName == "bb")
                },
                new Prop()
                {
                    Name = "SS Crewneck",
                    Description = "Short Sleeve Shirt. 50% Cotton, 50% Silk",
                    PropType = PropType.SS,
                    Hashtags = "#SS #T-shirt #50/50",
                    ImageUrl = GlobalConstants.SSCrewneck,
                    Price = 60,
                    PropCreator = this.context.Users.FirstOrDefault(u => u.UserName == "sashoto")
                },
                new Prop()
                {
                    Name = "Hoodie Front PKT",
                    Description = "Long Hoodie with Cangoroo Pocket in front. 100% Cotton",
                    PropType = PropType.Hoodies,
                    Hashtags = "#Hoodie #Cool #Cangoroo #sport",
                    ImageUrl = GlobalConstants.Hoodie,
                    Price = 70,
                    PropCreator = this.context.Users.FirstOrDefault(u => u.UserName == "aa")
                },
                new Prop()
                {
                    Name = "Bra X-trail",
                    Description = "Sport Bra with a nice X form. 40% Cotton, 60% Silk",
                    PropType = PropType.Bras,
                    Hashtags = "#BRA #Sport #Women",
                    ImageUrl = GlobalConstants.Bra,
                    Price = 40,
                    PropCreator = this.context.Users.FirstOrDefault(u => u.UserName == "bb")
                },
                new Prop()
                {
                    Name = "Winter Jogging Pants",
                    Description = "Long Pants for winter days. 60% Cotton, 30% Wool, 10% Silk",
                    PropType = PropType.Pants,
                    Hashtags = "#Pantsforwinter #winter #warm #nice #longpants",
                    ImageUrl = GlobalConstants.winterPants,
                    Price = 67.4,
                    PropCreator = this.context.Users.FirstOrDefault(u => u.UserName == "aa")
                },
                new Prop()
                {
                    Name = "Vest",
                    Description = "Winter Vest for warm condition with side zippers. 80% Silk ,20% cotton",
                    PropType = PropType.Vests,
                    Hashtags = "#Vest #niceAndWarmCoooool #zippers #double sided",
                    ImageUrl = GlobalConstants.Vest,
                    Price = 80,
                    PropCreator = this.context.Users.FirstOrDefault(u => u.UserName == "sashoto")
                },
            };

            this.context.Props.AddRange(props);
            this.context.SaveChanges();
        }

        //  MarvelousProps
        private void SeedMarvelousProps()
        {
            if (this.context.MarvelousProps.Any()) return;

            var marvelousProps = new List<MarvelousProp>()
            {
                new MarvelousProp()
                {
                    Name = "Space collection - Mars",
                    Description = "Collection of space suits models from Mars Expedition. Parachutes and landing balls.",
                    PropType = MarvelousType.Astronauts,
                    Hashtags = "#Mars #expedition #Suits #parachutes #balls",
                    ImageUrl = GlobalConstants.noimage,
                    Points = 1200,
                    MarvelousCreator = this.context.Users.FirstOrDefault(u => u.UserName == "owner")
                },
                new MarvelousProp()
                {
                    Name = "Park and chill",
                    Description = "Holiday equipment for lazy days. Ropes,hammocks,inflatable pillows.",
                    PropType = MarvelousType.Holiday,
                    Hashtags = "#holiday #lazy #hammocks",
                    ImageUrl = GlobalConstants.noimage,
                    Points = 1000,
                    MarvelousCreator = this.context.Users.FirstOrDefault(u => u.UserName == "bb")
                },
                new MarvelousProp()
                {
                    Name = "Divers suits",
                    Description = "Suits for diving expirience ,soft and waterproof! Keeps your body warm!",
                    PropType = MarvelousType.Divers,
                    Hashtags = "#Hoodie #Cool #Cangoroo #sport",
                    ImageUrl = GlobalConstants.noimage,
                    Points = 800,
                    MarvelousCreator = this.context.Users.FirstOrDefault(u => u.UserName == "bb")
                },
                new MarvelousProp()
                {
                    Name = "Landing on the moon suits",
                    Description = "First time landing on the moon! Neil Armstrong and Buzz Aldrin space suits!",
                    PropType = MarvelousType.Astronauts,
                    Hashtags = "#Moon #firsttime $Neil Armstrong #suits #Aldrin #Buzz",
                    ImageUrl = GlobalConstants.noimage,
                    Points = 1500,
                    MarvelousCreator = this.context.Users.FirstOrDefault(u => u.UserName == "bb")
                },
            };

            this.context.MarvelousProps.AddRange(marvelousProps);
            this.context.SaveChanges();
        }

        //  All stuff seeding at once
        public async Task SeedAllData()
        {
            Task.Run(SeedDatabase).Wait();
            Task.Run(SeedRoles).Wait();
            Task.Run(SeedAdmins).Wait();
            Task.Run(SeedUsers).Wait();
            Task.Run(SeedAccounts).Wait();
            Task.Run(UserAccountsChanges).Wait();
            Task.Run(SeedLicenzes).Wait();
            Task.Run(SeedProps).Wait();
            Task.Run(SeedMarvelousProps).Wait();
        }

        //  hash password 
        public void HashPaswword(List<FabricaUser> usersToDb, DbSet<FabricaUser> usersFromDb)
        {
            foreach (var currentUser in usersToDb)
            {
                if (usersFromDb.Any(user => user.UserName == currentUser.UserName) == false)
                {
                    var password = new PasswordHasher<FabricaUser>().HashPassword(currentUser, currentUser.PasswordHash);
                    currentUser.PasswordHash = password;
                }
            }
        }

        // set role , saveChanges
        private async Task CreateUserAddRole(List<FabricaUser> usersToDb, DbSet<FabricaUser> usersFromDb, string role)
        {
            foreach (var currentUser in usersToDb)
            {
                if (usersFromDb.Any(user => user.UserName == currentUser.UserName) == false)
                {
                    var userStore = new UserStore<FabricaUser>(context);
                    await userStore.CreateAsync(currentUser);
                    await userStore.AddToRoleAsync(currentUser, role);
                }
            }
        }

        // set role , saveChanges
        private void UserAccountsChanges()
        {
            var owner = this.context.Users.FirstOrDefault(x => x.UserName == "owner");
            var mainAdmin = this.context.Users.FirstOrDefault(x => x.UserName == "bb");
            var aa = this.context.Users.FirstOrDefault(x => x.UserName == "aa");

            //owner
            var ownerAccountId = this.context.CreditAccounts.FirstOrDefault(x => x.AccountOwnerId == owner.Id)?.AccountId;
            var ownerAccount = this.context.CreditAccounts.FirstOrDefault(x => x.AccountOwner == owner);
            //bb
            var adminAccountId = this.context.CreditAccounts.FirstOrDefault(x => x.AccountOwnerId == mainAdmin.Id)?.AccountId;
            var adminAccount = this.context.CreditAccounts.FirstOrDefault(x => x.AccountOwner == mainAdmin);
            //aa
            var aaAccountId = this.context.CreditAccounts.FirstOrDefault(x => x.AccountOwnerId == aa.Id)?.AccountId;
            var aaAccount = this.context.CreditAccounts.FirstOrDefault(x => x.AccountOwner == aa);

            if (owner != null)
            {
                owner.CreditAccountId = ownerAccountId;
                owner.CreditAccount = ownerAccount;
            }

            if (mainAdmin != null)
            {
                mainAdmin.CreditAccountId = adminAccountId;
                mainAdmin.CreditAccount = adminAccount;
            }

            if (aa != null)
            {
                aa.CreditAccountId = aaAccountId;
                aa.CreditAccount = aaAccount;
            }

            if (owner != null) this.context.Users.Update(owner);
            if (mainAdmin != null) this.context.Users.Update(mainAdmin);
            if (aa != null) this.context.Users.Update(aa);
            this.context.SaveChanges();
        }
    }
}
