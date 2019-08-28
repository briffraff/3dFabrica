namespace Fabrica.Services
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Contracts;
    using Data;
    using Fabrica.Infrastructure;
    using Fabrica.Infrastructure.Exceptions;
    using Fabrica.Models;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class MarvelousPropsService : DataService, IMarvelousPropsService
    {
        public MarvelousPropsService(FabricaDBContext context) : base(context)
        {
        }

        public async Task Create(MarvelousPropServiceModel model)
        {
            var exceptionMessage = "";

            try
            {
                var marvelousProp = Mapper.Map<MarvelousProp>(model);

                await this.context.MarvelousProps.AddAsync(marvelousProp);
                await this.context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                exceptionMessage = ex.Message;
                throw new CreatePropException();
            }
        }

        public async Task<IEnumerable<T>> GetAll<T>(bool isDeleted)
        {
            var props = this.context.MarvelousProps.
                Where(p => p.IsDeleted == isDeleted)
                .ProjectTo<T>();

            return props;
        }

        // GET Marvelous PROP
        public async Task<IEnumerable<T>> GetMarvelousProp<T>(string id)
        {
            var prop = this.context.MarvelousProps
                .Where(p => p.Id == id && p.IsDeleted == false)
                .ProjectTo<T>();

            return prop;
        }


        // DELETE
        public async Task Delete(string id)
        {
            var exceptionMessage = "";

            try
            {
                var product = await this.context.MarvelousProps.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);

                if (product == null)
                {
                    return;
                }

                product.IsDeleted = true;

                this.context.MarvelousProps.Update(product);
                await this.context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                exceptionMessage = ex.Message;
                throw new DeletePropException();
            }
        }
    }
}
