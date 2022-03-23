using System;
using System.Collections.Generic;

namespace MiniBank.Core.Domains.BankAccounts.Services
{
    public interface IBankAccountService
    {
        void Create(Account newAccount);
        void CloseAccount(Guid id);
        decimal CalculateComission(decimal sum,Guid fromUserId,Guid toUserId);
        void Remittance(decimal sum,Guid fromAccountId,Guid toAccountId);
        IEnumerable<Account> GetAllAccounts();
    }
}