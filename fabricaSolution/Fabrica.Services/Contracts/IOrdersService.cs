namespace Fabrica.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IOrdersService
    {
        Task<IEnumerable<T>> propsAll<T>();
        Task<IEnumerable<T>> marvsAll<T>();
        Task<IEnumerable<T>> PropsForUser<T>(string userId);
        Task<IEnumerable<T>> MarvsForUser<T>(string userId);
        Task<IEnumerable<T>> My<T>(string userId);
        Task<IEnumerable<T>> All<T>();
        Task AddToBasket(string productId, string userId);
        Task Cancel(string orderId);
        Task ConfirmAll(string userId);
    }
}