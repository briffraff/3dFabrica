namespace Fabrica.Services.Contracts
{
    using System.Collections.Generic;
    using Fabrica.Models.Enums;
    using Fabrica.Services.Models;
    using System.Threading.Tasks;

    public interface ICreditAccountsService
    {
        Task AddCreditCard(CreditAccountServiceModel model);
        Task LoadCash(string id, double cash);
        Task BuyLicense(string id, string licenzeType);
        Task<IEnumerable<T>> GetCurrentAccount<T>(string id);
    }
}
