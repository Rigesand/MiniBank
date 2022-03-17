using System.Collections.Generic;

namespace MiniBank.Core.Domains.BankAccounts.Repositories
{
    public interface IBankAccountRepository
    {
        void Create(Account newAccount);
        Account GetAccount(string accountId);
        void CloseAccount(string id);
        void Remittance(Account fromAccount, Account toAccount);
        bool AccountExists(string userId);
        List<Account> GetAllAccounts();
    }
}