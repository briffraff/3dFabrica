namespace Fabrica.Services.Contracts
{
    using Fabrica.Services.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICreditAccountsService
    {
        Task AddCreditCard(CreditAccountServiceModel model);
        Task<string> HashCreditCardNumber(string cardNumber);
        Task<string> CalcAuthNumber(string cardNumber);
        Task LoadCash(string id, double cash);
        Task BuyLicense(string id, string licenzeType);
        Task<IEnumerable<T>> GetCurrentAccount<T>(string id);
    }
}
