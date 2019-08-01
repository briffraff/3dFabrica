namespace Fabrica.Services
{
    using AutoMapper;
    using Data;
    using Fabrica.Models;
    using System.Threading.Tasks;
    using Contracts;
    using Models;

    public class MarvelousPropsService : DataService, IMarvelousPropsService
    {
        public MarvelousPropsService(FabricaDBContext context) : base(context)
        {
        }

        public async Task Create(MarvelousPropServiceModel model)
        {
            var marvelousprop = Mapper.Map<MarvelousProp>(model);

            await this.context.MarvelousProps.AddAsync(marvelousprop);
            await this.context.SaveChangesAsync();
        }
    }
}
