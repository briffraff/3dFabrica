namespace Fabrica.Services.Contracts
{
    using Fabrica.Models;
    using Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPropsService
    {
        Task Create(PropServiceModel model);
        Task Edit(PropServiceModel model);
        Task Delete(string id);
        Task Restore(string id);
        Task<IEnumerable<T>> GetUserProps<T>(string id);
        Task<IEnumerable<T>> GetProp<T>(string id);
        Task<IEnumerable<T>> GetDeletedProps<T>(string id);
        Task<Prop> GetDelProp(string id);
        Task<IEnumerable<T>> GetAll<T>(bool isDeleted);

    }
}
