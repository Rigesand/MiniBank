using System;
using System.Collections.Generic;

namespace MiniBank.Core.Domains.BankAccounts.Repositories
{
    public interface IBankAccountRepository
    {
        void Create(Account newAccount);
        Account GetAccount(Guid accountId);
        void CloseAccount(Guid id);
        void ChangeAmounts(Account fromAccount, Account toAccount);
        bool AccountExists(Guid userId);
        IEnumerable<Account> GetAllAccounts();
    }
}