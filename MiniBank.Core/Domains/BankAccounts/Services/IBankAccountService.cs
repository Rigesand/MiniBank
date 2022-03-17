using System.Collections.Generic;

namespace MiniBank.Core.Domains.BankAccounts.Services
{
    public interface IBankAccountService
    {
        void Create(Account newAccount);
        void CloseAccount(string id);
        decimal CalculateComission(decimal sum,string fromAccountId,string toAccountId);
        void Remittance(decimal sum,string fromAccountId,string toAccountId);
        List<Account> GetAllAccounts();
    }
}