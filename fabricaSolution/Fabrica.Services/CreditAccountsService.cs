namespace Fabrica.Services
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Fabrica.Data;
    using Fabrica.Infrastructure.Exceptions;
    using Fabrica.Models;
    using Fabrica.Services.Contracts;
    using Fabrica.Services.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;

    public class CreditAccountsService : DataService, ICreditAccountsService
    {
        public CreditAccountsService(FabricaDBContext context) : base(context)
        {
        }

        public async Task AddCreditCard(CreditAccountServiceModel model)
        {
            var exceptionMessage = "";

            try
            {
                var account = Mapper.Map<CreditAccount>(model);

                var chechIfAccountExists = this.context.CreditAccounts
                    .Any(x => x.CardNumber == model.CardNumber);

                var checkIfUserExists = this.context.CreditAccounts
                    .Any(a => a.AccountOwnerId == model.AccountOwnerId);

                if (chechIfAccountExists == true || checkIfUserExists == true)
                {
                    return;
                }

                //get auth number then pass to model
                account.AuthNumber = await this.CalcAuthNumber(model.CardNumber);

                //hash credit card number then pass to model
                account.CardNumber = await this.HashCreditCardNumber(model.CardNumber);

                await this.context.CreditAccounts.AddAsync(account);

                await this.context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                exceptionMessage = ex.Message;
                throw new AddCreditCardException();
            }
        }

        public async Task LoadCash(string id, double cash)
        {
            var exceptionMessage = "";

            try
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
            catch (Exception ex)
            {
                exceptionMessage = ex.Message;
                throw new LoadCashException();
            }

        }

        public async Task BuyLicense(string id, string licenseType)
        {
            var exceptionMessage = "";

            try
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
            catch (Exception ex)
            {
                exceptionMessage = ex.Message;
                throw new BuyLicenseException();
            }

        }

        public async Task<IEnumerable<T>> GetCurrentAccount<T>(string id)
        {
            var account = this.context.CreditAccounts.Where(a => a.AccountOwnerId == id).ProjectTo<T>();

            return account;
        }

        public async Task<string> HashCreditCardNumber(string cardNumber)
        {
            using (SHA512 sha512Hash = SHA512.Create())
            {
                var hashedCreditCardNumber =
                    Encoding.UTF8.GetString(sha512Hash.ComputeHash(Encoding.UTF8.GetBytes(cardNumber)));

                return hashedCreditCardNumber;
            }
        }

        public async Task<string> CalcAuthNumber(string cardNumber)
        {
            var split = cardNumber.Split("-".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToArray();
            var authNumber = split[3];

            return authNumber;
        }
    }
}
