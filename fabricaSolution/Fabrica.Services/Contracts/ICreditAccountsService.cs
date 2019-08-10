namespace Fabrica.Services.Contracts
{
    using Fabrica.Services.Models;
    using System.Threading.Tasks;

    public interface ICreditAccountsService
    {
        Task AddCreditCard(CreditAccountServiceModel model);
        Task LoadCash(string id, double cash);
    }
}
