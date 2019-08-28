namespace Fabrica.Services
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Contracts;
    using Data;
    using Fabrica.Infrastructure;
    using Fabrica.Infrastructure.Exceptions;
    using Fabrica.Models;
    using Fabrica.Models.Enums;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class PropsService : DataService, IPropsService
    {

        public PropsService(FabricaDBContext context) : base(context)
        {
        }

        // CREATE
        public async Task Create(PropServiceModel model)
        {
            var exceptionMessage = "";

            try
            {
                var prop = Mapper.Map<Prop>(model);

                ////var creator = await this.context.Users.FirstOrDefaultAsync(x => x.UserName == model.PropCreator.UserName);
                ////prop.PropCreator = creator;

                await this.context.Props.AddAsync(prop);
                await this.context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                exceptionMessage = ex.Message;
                throw new CreatePropException();
            }
        }

        // EDIT
        public async Task Edit(PropServiceModel model)
        {
            var exceptionMessage = "";

            try
            {
                var prop = await this.context.Props.FirstOrDefaultAsync(p => p.Id == model.Id && !p.IsDeleted);

                if (prop == null)
                {
                    return;
                }

                prop.ImageUrl = model.ImageUrl;
                prop.Name = model.Name;
                prop.PropType = Mapper.Map<PropType>(model.PropType);
                prop.Description = model.Description;
                prop.Price = model.Price;
                prop.Hashtags = model.Hashtags;

                this.context.Props.Update(prop);
                await this.context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                exceptionMessage = ex.Message;
                throw new EditPropException();
            }
        }

        // DELETE
        public async Task Delete(string id)
        {
            var exceptionMessage = "";

            try
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
            catch (Exception ex)
            {
                exceptionMessage = ex.Message;
                throw new DeletePropException();
            }
        }

        // ACTIVATE
        public async Task Restore(string id)
        {
            var exceptionMessage = "";

            try
            {
                var product = await this.context.Props.FirstOrDefaultAsync(p => p.Id == id && p.IsDeleted == true);

                if (product == null)
                {
                    return;
                }

                product.IsDeleted = false;

                this.context.Props.Update(product);
                await this.context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                exceptionMessage = ex.Message;
                throw new RestorePropException();
            }
        }

        // GET USER PROPS
        public async Task<IEnumerable<T>> GetUserProps<T>(string id)
        {
            var props = await this.context.Props
                .Where(u => u.PropCreatorId == id && u.IsDeleted == false)
                .ProjectTo<T>()
                .ToArrayAsync();

            return props;
        }

        // GET PROP
        public async Task<IEnumerable<T>> GetProp<T>(string id)
        {
            var prop = this.context.Props.Where(p => p.Id == id && p.IsDeleted == false).ProjectTo<T>();

            return prop;
        }

        // GET USER DELETED PROPS
        public async Task<IEnumerable<T>> GetDeletedProps<T>(string id)
        {
            var props = await this.context.Props.Where(u => u.PropCreatorId == id && u.IsDeleted == true).ProjectTo<T>().ToArrayAsync();

            return props;
        }

        // GET USER DELETED PROP
        public async Task<Prop> GetDelProp(string id)
        {
            var prop = await this.context.Props.FirstOrDefaultAsync(p => p.Id == id && p.IsDeleted == true);
            return prop;
        }

        public async Task<IEnumerable<T>> GetAll<T>(bool isDeleted)
        {
            var props = await this.context.Props.Where(p => p.IsDeleted == isDeleted).ProjectTo<T>().ToArrayAsync();
            return props;
        }

    }
}
