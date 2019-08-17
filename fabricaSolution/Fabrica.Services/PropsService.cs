using Fabrica.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Fabrica.Services
{
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Fabrica.Models;
    using Models;
    using Data;
    using Contracts;
    using System.Collections.Generic;
    using AutoMapper.QueryableExtensions;

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

        public async Task Edit(PropServiceModel model)
        {
            var prop = await this.context.Props.FirstOrDefaultAsync(p => p.Id == model.Id && !p.IsDeleted);

            if (prop == null)
            {
                return;
            }

            prop.ImageUrl = model.ImageUrl;
            prop.Name = model.Name;
            prop.Type = Mapper.Map<PropType>(model.Type);
            prop.Description = model.Description;
            prop.Price = model.Price;
            prop.Hashtags = model.Hashtags;

            this.context.Props.Update(prop);
            await this.context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var product = await this.context.Props.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);

            if (product == null)
            {
                return;
            }

            product.IsDeleted = true;

            this.context.Props.Update(product);
            await this.context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetUserProps<T>(string id)
        {
            var props = this.context.Props.Where(u => u.PropCreatorId == id).ProjectTo<T>();
            return props;
        }

        public async Task<Prop> GetProp(string id)
        {
            var prop = await this.context.Props.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);

            return prop;
        }

        //public async Task<FabricaUser> GetPropCreator(string propId)
        //{
        //    var user = this.context.Props.Select(x => x.PropCreatorId).Where(c => c.Contains(propId));

        //    var map = Mapper.Map<FabricaUser>(user);

        //    return map;
        //}

    }
}
