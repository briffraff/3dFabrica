namespace Fabrica.Services
{
    using Fabrica.Models;
    using System.Collections.Generic;
    using System.Linq;
    using Fabrica.Data;
    using Fabrica.Services.Contracts;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

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
    }
}
