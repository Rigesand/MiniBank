using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MiniBank.Core.Domains.BankAccounts;
using MiniBank.Core.Domains.BankAccounts.Repositories;
using MiniBank.Core.Exception;

namespace MiniBank.Data.Accounts.Repository
{
    public class BankAccountRepository: IBankAccountRepository
    {
        
        public static List<AccountDbModel> BankAccounts = new List<AccountDbModel>();

        private readonly IMapper _mapper;

        public BankAccountRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public void Create(Account newAccount)
        {
            var dbAccount = _mapper.Map<Account, AccountDbModel>(newAccount);
            BankAccounts.Add(dbAccount);
        }

        public Account GetAccount(string accountId)
        {
            if (string.IsNullOrEmpty(accountId))
                throw new ValidationException("id не может быть пустым или null");
            var dbAccount = BankAccounts.FirstOrDefault(it => it.Id == accountId);

            if (dbAccount==null)
                throw new ValidationException("Такого аккаунта не существует");
            
            var coreAccount=_mapper.Map<AccountDbModel, Account>(dbAccount);

            return coreAccount;
        }

        public void CloseAccount(string id)
        {
            var dbAccount = BankAccounts.Find(it => it.Id == id);
            if (dbAccount is null)
                throw new ValidationException("Аккаунта с таким id не существует");

            if (dbAccount.Sum!=0)
                throw new ValidationException("Не удалось закрыть аккаунт,так как на счету имеются средства");
            
            dbAccount.IsActive = false;
            dbAccount.ClosingDate=DateTime.Now;
        }

        public void Remittance(Account fromAccount, Account toAccount)
        {
            var fromAccountIndex = BankAccounts.FindIndex(it => it.Id == fromAccount.Id);
            var toAccountIndex = BankAccounts.FindIndex(it => it.Id == toAccount.Id);

            BankAccounts[fromAccountIndex].Sum = fromAccount.Sum;
            BankAccounts[toAccountIndex].Sum = toAccount.Sum;
        }


        public bool AccountExists(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                throw new ValidationException("id не может быть пустым или null");
            var isExists = BankAccounts.Find(it => it.UserId == userId);
            if (isExists is not null)
                return true;
            return false;
        }

        public List<Account> GetAllAccounts()
        {
            return _mapper.Map<List<AccountDbModel>, List<Account>>(BankAccounts);
        }
    }
}