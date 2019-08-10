using System.Linq;

namespace Fabrica.Services
{
    using AutoMapper;
    using Fabrica.Data;
    using Fabrica.Models;
    using Fabrica.Services.Contracts;
    using Fabrica.Services.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    public class CreditAccountsService : DataService, ICreditAccountsService
    {
        public CreditAccountsService(FabricaDBContext context) : base(context)
        {
        }

        public async Task AddCreditCard(CreditAccountServiceModel model)
        {
            var account = Mapper.Map<CreditAccount>(model);

            if(this.context.CreditAccounts.Any()) return;

            await this.context.CreditAccounts.AddAsync(account);

            await this.context.SaveChangesAsync();
        }

        public async Task LoadCash(string id, double cash)
        {
            var account = await this.context.CreditAccounts.FirstOrDefaultAsync(a => a.AccountOwnerId == id);

            if (account == null)
            {
                return;
            }

            account.Cash += cash;

            this.context.CreditAccounts.Update(account);
            await this.context.SaveChangesAsync();
        }

        public async Task BuyLicense(string id, string licenseType)
        {
            var account = await this.context.CreditAccounts.FirstOrDefaultAsync(a => a.AccountOwnerId == id);

            if (account == null)
            {
                return;
            }

            var chosenLicenze = await this.context.Licenzes.FirstOrDefaultAsync(l => l.Type.ToString() == licenseType);

            var minusCash = chosenLicenze.Price;
            var plusPoints = chosenLicenze.bonusPoints;

            if (account.Cash >= minusCash)
            {
                account.Cash -= minusCash;
                account.Points += plusPoints;
            }

            this.context.CreditAccounts.Update(account);
            await this.context.SaveChangesAsync();
        }

    }
}
