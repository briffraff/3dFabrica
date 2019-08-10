namespace Fabrica.Services
{
    using Fabrica.Data;
    using Fabrica.Models;
    using Fabrica.Services.Contracts;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class UsersService : DataService, IUsersService
    {
        public UsersService(FabricaDBContext context) : base(context)
        {
            
        }

        public async Task<List<FabricaUser>> GetAllUsers()
        {
            var allUsers = await this.context.Users.ToListAsync();

            return allUsers;
        }
        
        public async Task<FabricaUser> GetUser(string username)
        {
            var currentUser = await this.context.Users.SingleOrDefaultAsync(user => user.UserName == username);

            return currentUser;
        }

        public async Task GetAccountIdAndSetToUser(string username,string id)
        {
            var account = await this.context.CreditAccounts.FirstOrDefaultAsync(a=>a.AccountOwnerId == id);

            if (account == null)
            {
                return;
            }

            var user = this.GetUser(username);

            user.Result.CreditAccountId = account.AccountId;

            this.context.Users.Update(await user);
            await this.context.SaveChangesAsync();
        }

        public async Task<CreditAccount> GetAccountOfTheCurrentUser(string id)
        {
            var account = await this.context.CreditAccounts.FirstOrDefaultAsync(a => a.AccountOwnerId == id);

            return account;
        }



    }
}
