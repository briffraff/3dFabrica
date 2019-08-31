namespace Fabrica.Services
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Fabrica.Data;
    using Fabrica.Infrastructure;
    using Fabrica.Infrastructure.Exceptions;
    using Fabrica.Models;
    using Fabrica.Services.Contracts;
    using Fabrica.Services.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class UsersService : DataService, IUsersService
    {
        public UsersService(FabricaDBContext context) : base(context)
        {
        }

        public async Task<FabricaUserServiceModel> Get(string id)
        {
            var user = await this.context.Users
                .ProjectTo<FabricaUserServiceModel>()
                .FirstOrDefaultAsync(p => p.Id == id && p.IsDeleted == false);

            var viewModel = Mapper.Map<FabricaUserServiceModel>(user);

            return viewModel;
        }

        public async Task<FabricaUser> GetUser(string username)
        {
            var currentUser = await this.context.Users.SingleOrDefaultAsync(user => user.UserName == username);

            return currentUser;
        }

        public async Task<List<FabricaUser>> GetAllUsers()
        {
            var allUsers = await this.context.Users
                //.Where(u => u.IsDeleted == false)
                .ToListAsync();

            return allUsers;
        }

        public async Task GetAccountIdAndSetToUser(string username, string id)
        {
            var account = await this.context.CreditAccounts.FirstOrDefaultAsync(a => a.AccountOwnerId == id);

            if (account == null)
            {
                return;
            }

            var user = this.GetUser(username);

            user.Result.CreditAccountId = account.AccountId;

            this.context.Users.Update(await user);
            await this.context.SaveChangesAsync();
        }

        public async Task DeactivateUser(string id)
        {
            var exceptionMessage = "";

            try
            {
                var user = await this.context.Users.FirstOrDefaultAsync(u => u.Id == id && u.IsDeleted == false);

                if (user == null)
                {
                    return;
                }

                //set password = string + last char of the username.toupper
                var deactivationPassword = string.Format
                    ($"{GlobalConstants.deactivationPass}", (user.UserName[user.UserName.Length - 1].ToString().ToUpper()));

                var password = new PasswordHasher<FabricaUser>().HashPassword(user, deactivationPassword);

                user.IsDeleted = true;
                user.PasswordHash = password;

                this.context.Users.Update(user);
                await this.context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                exceptionMessage = ex.Message;
                throw new DeactivateUserException();
            }
        }

        public async Task ActivateUser(string id)
        {
            var exceptionMessage = "";

            try
            {
                var user = await this.context.Users.FirstOrDefaultAsync(u => u.Id == id && u.IsDeleted == true);

                if (user == null)
                {
                    return;
                }

                //set password = string + first char of the user + last char of the username.toupper
                var activationPassword = string.Format
                ($"{GlobalConstants.activationPass}", (user.UserName[0]), (user.UserName[user.UserName.Length - 1].ToString().ToUpper()));

                var password = new PasswordHasher<FabricaUser>().HashPassword(user, activationPassword);

                user.IsDeleted = false;
                user.PasswordHash = password;

                this.context.Users.Update(user);
                await this.context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                exceptionMessage = ex.Message;
                throw new ActivateUserException();
            }
        }

    }
}
