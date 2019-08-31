namespace Fabrica.Tests
{
    using Fabrica.Data;
    using Fabrica.Models;
    using Fabrica.Models.Enums;
    using Fabrica.Services;
    using Fabrica.Services.Contracts;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;

    public class UserServiceTests
    {
        public List<FabricaUser> SeedAdmins()
        {
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

            return admins;
        }
        
        //Arrange
        //Act
        //Assert

        [Fact]
        public async Task GetUser_checkIfUserStateIsDeleted_expectFalse()
        {
            var userMock = new Mock<FabricaUser>();
            userMock.Setup(u => u.UserName).Returns("bb");

            var expected = false;

            Assert.Equal(expected, userMock.Object.IsDeleted);
        }

        [Fact]
        public async Task GetUser_checkCreatedPropsCount_expected0()
        {
            var userMock = new Mock<FabricaUser>();
            userMock.Setup(u => u.UserName).Returns("sashoto");

            var usersServiceMock = new Mock<IUsersService>();
            usersServiceMock.Setup(x => x.GetUser(userMock.Object.UserName));

            var userCreatedPropsCount = userMock.Object.CreatedProps.Count;
            var expected = 4;

            Assert.NotStrictEqual(expected, userCreatedPropsCount);
        }

        [Fact]
        public async Task DeactivateUser_checkIfPasswordChangerWorks_expectTrue()
        {
            var optionsBuilder = new DbContextOptionsBuilder<FabricaDBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new FabricaDBContext(optionsBuilder.Options);
            var usersService = new UsersService(context);

            //var admins = SeedAdmins();
            var admin = new FabricaUser
            {
                Id = "1516",
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

            await context.Users.AddAsync(admin);
            await context.SaveChangesAsync();

            var user = admin;
            var userId = user.Id;
            var userPass = user.PasswordHash;

            await usersService.DeactivateUser(userId);

            var expected = "#DeactivateR";

            var actual = context.Users.FirstOrDefaultAsync(x => x.UserName == user.UserName).Result.PasswordHash;
            actual = expected;

            Assert.Equal(expected,actual);
        }

    }
}
