namespace Fabrica.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IOrdersService
    {
        Task<IEnumerable<T>> GetAll<T>();

        Task AddToBasket(string productId, string userId);
    }
}