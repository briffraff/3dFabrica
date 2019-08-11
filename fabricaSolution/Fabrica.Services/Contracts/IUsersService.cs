namespace Fabrica.Services.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;
    using Fabrica.Models;
    using System.Collections.Generic;

    public interface IUsersService
    {
        Task<List<FabricaUser>> GetAllUsers();
        Task<FabricaUser> GetUser(string username);
        Task GetAccountIdAndSetToUser(string username, string id);
    }
}
