
using System.Collections.Generic;
using AutoMapper.QueryableExtensions;

namespace Fabrica.Services
{
    using System.Linq;
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

        public async Task<IEnumerable<T>> GetUserProps<T>(string id)
        {
            var props = this.context.Props.Where(u => u.PropCreatorId == id).ProjectTo<T>();
            return props;
        }

    }
}
