namespace Fabrica.Services.Contracts
{
    using System.Collections.Generic;
    using System.Linq;
    using Fabrica.Models;
    using Models;
    using System.Threading.Tasks;

    public interface IPropsService
    {
        Task Create(PropServiceModel model);
        Task Edit(PropServiceModel model);
        Task Delete(string id);
        Task<IEnumerable<T>> GetUserProps<T>(string id);
        Task<Prop> GetProp(string id);

        //Task<FabricaUser> GetPropCreator(string propId);

    }
}
