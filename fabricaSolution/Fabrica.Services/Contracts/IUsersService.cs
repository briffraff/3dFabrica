namespace Fabrica.Services.Contracts
{
    using Fabrica.Models;
    using Fabrica.Services.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

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
