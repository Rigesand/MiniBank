using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniBank.Core.Domains.BankAccounts.Repositories
{
    public interface IBankAccountRepository
    {
        Task Create(Account newAccount);
        Task<Account> GetAccount(Guid accountId);
        Task CloseAccount(Guid id);
        Task ChangeAmounts(Account fromAccount, Account toAccount);
        Task<bool> AccountExists(Guid userId);
        Task<IEnumerable<Account>> GetAllAccounts();
    }
}