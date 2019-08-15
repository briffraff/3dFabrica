namespace Fabrica.Services.Contracts
{
    using System.Linq;
    using Fabrica.Services.Models;
    using System.Threading.Tasks;
    using Fabrica.Models;
    using System.Collections.Generic;

    public interface IUsersService
    {
        Task<FabricaUserServiceModel> Get(string id);
        Task<FabricaUser> GetUser(string username);
        Task<List<FabricaUser>> GetAllUsers();
        Task GetAccountIdAndSetToUser(string username, string id);
        Task DeactivateUser(string id);
        Task ActivateUser(string id);
    }
}
