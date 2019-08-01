namespace Fabrica.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IPropsService
    {
        Task Create(PropServiceModel model);

        //Task<PropServiceModel> Get(string id);

        //Task Edit(PropServiceModel model);

        //Task Delete(string id);

        //Task<IEnumerable<PropServiceModel>> GetAll();
    }
}
