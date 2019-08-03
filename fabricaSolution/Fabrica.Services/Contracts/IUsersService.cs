
namespace Fabrica.Services.Contracts
{
    using System.Threading.Tasks;
    using Fabrica.Models;
    using System.Collections.Generic;

    public interface IUsersService
    {
        Task<List<FabricaUser>> GetAllUsers();
        Task<FabricaUser> GetUser(string username);
    }
}
