using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniBank.Core.Domains.BankAccounts.Services
{
    public interface IBankAccountService
    {
        Task Create(Account newAccount);
        Task CloseAccount(Guid id);
        decimal CalculateComission(decimal sum,Guid fromUserId,Guid toUserId);
        Task Remittance(decimal sum,Guid fromAccountId,Guid toAccountId);
        Task<IEnumerable<Account>> GetAllAccounts();
    }
}