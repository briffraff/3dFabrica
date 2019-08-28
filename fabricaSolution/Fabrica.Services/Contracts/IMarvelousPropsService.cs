namespace Fabrica.Services.Contracts
{
    using Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMarvelousPropsService
    {
        Task Create(MarvelousPropServiceModel model);
        Task Delete(string id);
        Task<IEnumerable<T>> GetAll<T>(bool isDeleted);
        Task<IEnumerable<T>> GetMarvelousProp<T>(string id);
    }
}
