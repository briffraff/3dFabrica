namespace Fabrica.Services
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Fabrica.Models;
    using Models;
    using Data;
    using Contracts;

    public class PropsService : DataService, IPropsService
    {
        public PropsService(FabricaDBContext context) : base(context)
        {
        }

        public async Task Create(PropServiceModel model)
        {
            var prop = Mapper.Map<Prop>(model);

            await this.context.Props.AddAsync(prop);
            await this.context.SaveChangesAsync();
        }

    }
}
