﻿namespace Fabrica.Services.Contracts
{
    using Models;
    using System.Threading.Tasks;

    public interface IPropsService
    {
        Task Create(PropServiceModel model);

        //Task<PropServiceModel> Get(string id);

        //Task Edit(PropServiceModel model);

        //Task Delete(string id);

        //Task<IEnumerable<PropServiceModel>> GetAll();
    }
}
