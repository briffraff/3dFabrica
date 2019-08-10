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
    using System.Threading.Tasks;

    public class FabricaDbSeedData
    {
        private readonly FabricaDBContext context;
        private readonly IApplicationBuilder app;
        private readonly IHostingEnvironment env;

        public FabricaDbSeedData(FabricaDBContext context, IApplicationBuilder app, IHostingEnvironment env)
        {
            this.context = context;
            this.app = app;
            this.env = env;
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
                }
            };

            HashPaswword(admins, usersFromDb);
            CreateUserAddRole(admins, usersFromDb, adminRole).Wait();
            this.context.SaveChanges();
        }

        //  users
        private void SeedUsers()
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

            var accounts = new List<CreditAccount>()
            {
                new CreditAccount()
                {
                    CardNumber = "2334-3344-3345-2333",
                    Points = 1700,
                    Cash = 102,
                    AccountOwner = this.context.Users.FirstOrDefault(u=>u.UserName == "owner")
                },
                new CreditAccount()
                {
                    CardNumber = "4444-5555-6666-7777",
                    Points = 1500,
                    Cash = 245,
                    AccountOwner = this.context.Users.FirstOrDefault(u=>u.UserName == "bb")
                }
            };

            this.context.CreditAccounts.AddRange(accounts);
            this.context.SaveChanges();
        }

        private void SeedLicenzes()
        {
            if(this.context.Licenzes.Any()) return;

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
                    Type = PropType.LS,
                    Hashtags = "#LS #Pullover #Cotton",
                    ImageUrl = GlobalConstants.LSCrewneck,
                    Price = 56.5,
                    PropCreator = this.context.Users.FirstOrDefault(u => u.UserName == "bb")
                },
                new Prop()
                {
                    Name = "SS Crewneck",
                    Description = "Short Sleeve Shirt. 50% Cotton, 50% Silk",
                    Type = PropType.SS,
                    Hashtags = "#SS #T-shirt #50/50",
                    ImageUrl = GlobalConstants.SSCrewneck,
                    Price = 60,
                    PropCreator = this.context.Users.FirstOrDefault(u => u.UserName == "sashoto")
                },
                new Prop()
                {
                    Name = "Hoodie Front PKT",
                    Description = "Long Hoodie with Cangoroo Pocket in front. 100% Cotton",
                    Type = PropType.Hoodies,
                    Hashtags = "#Hoodie #Cool #Cangoroo #sport",
                    ImageUrl = GlobalConstants.Hoodie,
                    Price = 70,
                    PropCreator = this.context.Users.FirstOrDefault(u => u.UserName == "aa")
                },
                new Prop()
                {
                    Name = "Bra X-trail",
                    Description = "Sport Bra with a nice X form. 40% Cotton, 60% Silk",
                    Type = PropType.Bras,
                    Hashtags = "#BRA #Sport #Women",
                    ImageUrl = GlobalConstants.Bra,
                    Price = 40,
                    PropCreator = this.context.Users.FirstOrDefault(u => u.UserName == "bb")
                },
                new Prop()
                {
                    Name = "Winter Jogging Pants",
                    Description = "Long Pants for winter days. 60% Cotton, 30% Wool, 10% Silk",
                    Type = PropType.Pants,
                    Hashtags = "#Pantsforwinter #winter #warm #nice #longpants",
                    ImageUrl = GlobalConstants.winterPants,
                    Price = 67.4,
                    PropCreator = this.context.Users.FirstOrDefault(u => u.UserName == "aa")
                },
                new Prop()
                {
                    Name = "Vest",
                    Description = "Winter Vest for warm condition with side zippers. 80% Silk ,20% cotton",
                    Type = PropType.Vests,
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
                    Type = MarvelousType.Astronauts,
                    Hashtags = "#Mars #expedition #Suits #parachutes #balls",
                    ImageUrl = GlobalConstants.noimage,
                    Points = 1200,
                    MarvelousCreator = this.context.Users.FirstOrDefault(u => u.UserName == "owner")
                },
                new MarvelousProp()
                {
                    Name = "Park and chill",
                    Description = "Holiday equipment for lazy days. Ropes,hammocks,inflatable pillows.",
                    Type = MarvelousType.Holiday,
                    Hashtags = "#holiday #lazy #hammocks",
                    ImageUrl = GlobalConstants.noimage,
                    Points = 1000,
                    MarvelousCreator = this.context.Users.FirstOrDefault(u => u.UserName == "bb")
                },
                new MarvelousProp()
                {
                    Name = "Divers suits",
                    Description = "Suits for diving expirience ,soft and waterproof! Keeps your body warm!",
                    Type = MarvelousType.Divers,
                    Hashtags = "#Hoodie #Cool #Cangoroo #sport",
                    ImageUrl = GlobalConstants.noimage,
                    Points = 800,
                    MarvelousCreator = this.context.Users.FirstOrDefault(u => u.UserName == "bb")
                },
                new MarvelousProp()
                {
                    Name = "Landing on the moon suits",
                    Description = "First time landing on the moon! Neil Armstrong and Buzz Aldrin space suits!",
                    Type = MarvelousType.Astronauts,
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
            //Task.Run(SeedAccounts).Wait();
            Task.Run(SeedLicenzes).Wait();
            Task.Run(SeedProps).Wait();
            Task.Run(SeedMarvelousProps).Wait();
        }

        //  hash password 
        private void HashPaswword(List<FabricaUser> usersToDb, DbSet<FabricaUser> usersFromDb)
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
    }
}
