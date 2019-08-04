namespace Fabrica.Data.Seeds
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Fabrica.Models;
    using Fabrica.Infrastructure;
    using Fabrica.Models.enums;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

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
                    Gender = GenderType.Male
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
                    Gender = GenderType.Male
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
                    Gender = GenderType.Male
                }
            };

            HashPaswwordSetRoleSaveToContext(admins, usersFromDb, adminRole);
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
                    Gender = GenderType.Female
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
                    Gender = GenderType.Male
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
                    Gender = GenderType.Male
                },
            };

            HashPaswwordSetRoleSaveToContext(usersToDb, usersFromDb, userRole);
        }

        // Props
        private void SeedProps()
        {
            var props = new List<Prop>()
            {
                new Prop()
                {
                    Name = "LS Crewneck",
                    Description = "Long Sleeve Pullover. 90% Cotton, 10% Silk",
                    Type = PropType.LS,
                    Hashtags = "#LS #Pullover #Cotton",
                    ImageUrl = "https://image.bg",
                    Price = 56.5,
                    PropCreator = this.context.Users.FirstOrDefault(u => u.UserName == "bb")
                },
                new Prop()
                {
                    Name = "SS Crewneck",
                    Description = "Short Sleeve Shirt. 50% Cotton, 50% Silk",
                    Type = PropType.SS,
                    Hashtags = "#SS #T-shirt #50/50",
                    ImageUrl = "https://image.bg",
                    Price = 60,
                    PropCreator = this.context.Users.FirstOrDefault(u => u.UserName == "sashoto")
                },
                new Prop()
                {
                    Name = "Hoodie Front PKT",
                    Description = "Long Hoodie with Cangoroo Pocket in front. 100% Cotton",
                    Type = PropType.Hoodies,
                    Hashtags = "#Hoodie #Cool #Cangoroo #sport",
                    ImageUrl = "https://image.bg",
                    Price = 70,
                    PropCreator = this.context.Users.FirstOrDefault(u => u.UserName == "aa")
                },
                new Prop()
                {
                    Name = "Bra X-trail",
                    Description = "Sport Bra with a nice X form. 40% Cotton, 60% Silk",
                    Type = PropType.Bras,
                    Hashtags = "#BRA #Sport #Women",
                    ImageUrl = "https://image.bg",
                    Price = 40,
                    PropCreator = this.context.Users.FirstOrDefault(u => u.UserName == "bb")
                },
                new Prop()
                {
                    Name = "Winter Jogging Pants",
                    Description = "Long Pants for winter days. 60% Cotton, 30% Wool, 10% Silk",
                    Type = PropType.Pants,
                    Hashtags = "#Pantsforwinter #winter #warm #nice #longpants",
                    ImageUrl = "https://image.bg",
                    Price = 67.4,
                    PropCreator = this.context.Users.FirstOrDefault(u => u.UserName == "aa")
                },
                new Prop()
                {
                    Name = "Vest",
                    Description = "Winter Vest for warm condition with side zippers. 80% Silk ,20% cotton",
                    Type = PropType.Vests,
                    Hashtags = "#Vest #niceAndWarmCoooool #zippers #double sided",
                    ImageUrl = "https://image.bg",
                    Price = 80,
                    PropCreator = this.context.Users.FirstOrDefault(u => u.UserName == "sashoto")
                },
            };

            this.context.AddRange(props);
            this.context.SaveChanges();

        }

        // MarvelousProps
        private void SeedMarvelousProps()
        {
            var marvelousProps = new List<MarvelousProp>()
            {
                new MarvelousProp()
                {
                    Name = "Space collection - Mars",
                    Description = "Collection of space suits models from Mars Expedition. Parachutes and landing balls.",
                    Type = MarvelousType.Astronauts,
                    Hashtags = "#Mars #expedition #Suits #parachutes #balls",
                    ImageUrl = "https://image.bg",
                    Points = 1200,
                    MarvelousCreator = this.context.Users.FirstOrDefault(u => u.UserName == "owner")
                },
                new MarvelousProp()
                {
                    Name = "Park and chill",
                    Description = "Holiday equipment for lazy days. Ropes,hammocks,inflatable pillows.",
                    Type = MarvelousType.Holiday,
                    Hashtags = "#holiday #lazy #hammocks",
                    ImageUrl = "https://image.bg",
                    Points = 1000,
                    MarvelousCreator = this.context.Users.FirstOrDefault(u => u.UserName == "bb")
                },
                new MarvelousProp()
                {
                    Name = "Divers suits",
                    Description = "Suits for diving expirience ,soft and waterproof! Keeps your body warm!",
                    Type = MarvelousType.Divers,
                    Hashtags = "#Hoodie #Cool #Cangoroo #sport",
                    ImageUrl = "https://image.bg",
                    Points = 800,
                    MarvelousCreator = this.context.Users.FirstOrDefault(u => u.UserName == "bb")
                },
                new MarvelousProp()
                {
                    Name = "Landing on the moon suits",
                    Description = "First time landing on the moon! Neil Armstrong and Buzz Aldrin space suits!",
                    Type = MarvelousType.Astronauts,
                    Hashtags = "#Moon #firsttime $Neil Armstrong #suits #Aldrin #Buzz",
                    ImageUrl = "https://image.bg",
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
            Task.Run(SeedProps).Wait();
            Task.Run(SeedMarvelousProps).Wait();
        }

        //  hash password , set role , saveChanges
        private async void HashPaswwordSetRoleSaveToContext(List<FabricaUser> usersToDb, DbSet<FabricaUser> usersFromDb, string role)
        {
            foreach (var currentUser in usersToDb)
            {
                if (usersFromDb.Any(user => user.UserName == currentUser.UserName) == false)
                {
                    var password = new PasswordHasher<FabricaUser>().HashPassword(currentUser, currentUser.PasswordHash);
                    currentUser.PasswordHash = password;
                    var userStore = new UserStore<FabricaUser>(context);
                    await userStore.CreateAsync(currentUser);
                    await userStore.AddToRoleAsync(currentUser, role);
                }
                await context.SaveChangesAsync();
            }
        }

    }
}
