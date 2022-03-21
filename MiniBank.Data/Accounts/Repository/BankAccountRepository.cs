using System;
using System.Collections.Concurrent;
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
        
        private static BlockingCollection<AccountDbModel> BankAccounts = new BlockingCollection<AccountDbModel>();

        private readonly IMapper _mapper;

        public BankAccountRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public void Create(Account newAccount)
        {
            var dbAccount = _mapper.Map<Account, AccountDbModel>(newAccount);
            dbAccount.Id=Guid.NewGuid();
            dbAccount.OpeningDate = DateTime.UtcNow;
            BankAccounts.Add(dbAccount);
        }

        public Account GetAccount(Guid accountId)
        {
            var dbAccount = BankAccounts.FirstOrDefault(it => it.Id == accountId);

            if (dbAccount==null)
                throw new ValidationException("Такого аккаунта не существует");
            
            var coreAccount=_mapper.Map<AccountDbModel, Account>(dbAccount);

            return coreAccount;
        }

        public void CloseAccount(Guid id)
        {
            var dbAccount = BankAccounts.FirstOrDefault(it => it.Id == id);
            if (dbAccount is null)
                throw new ValidationException("Аккаунта с таким id не существует");

            if (dbAccount.Sum!=0)
                throw new ValidationException("Не удалось закрыть аккаунт,так как на счету имеются средства");
            
            dbAccount.IsActive = false;
            dbAccount.ClosingDate=DateTime.UtcNow;
        }

        public void ChangeAmounts(Account fromAccount, Account toAccount)
        {
            var fromAccountDb = BankAccounts.FirstOrDefault(it => it.Id == fromAccount.Id);
            var toAccountDb = BankAccounts.FirstOrDefault(it => it.Id == toAccount.Id);

            fromAccountDb.Sum = fromAccount.Sum;
            toAccountDb.Sum = toAccount.Sum;
        }


        public bool AccountExists(Guid userId)
        {
            var isExists = BankAccounts.Any(it => it.UserId == userId);
            if (isExists)
                return true;
            return false;
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            return _mapper.Map<BlockingCollection<AccountDbModel>, IEnumerable<Account>>(BankAccounts);
        }
    }
}