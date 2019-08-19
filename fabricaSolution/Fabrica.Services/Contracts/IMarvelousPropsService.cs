namespace Fabrica.Services.Contracts
{
    using System.Collections.Generic;
    using Fabrica.Models;
    using Models;
    using System.Threading.Tasks;

    public interface IMarvelousPropsService
    {
        Task Create(MarvelousPropServiceModel model);
        Task<IEnumerable<T>> GetAll<T>(bool isDeleted);
        Task<IEnumerable<T>> GetMarvelousProp<T>(string id);
    }
}
