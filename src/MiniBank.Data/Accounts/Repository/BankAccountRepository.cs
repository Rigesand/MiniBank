using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MiniBank.Core.Domains.BankAccounts;
using MiniBank.Core.Domains.BankAccounts.Repositories;
using MiniBank.Core.Exception;

namespace MiniBank.Data.Accounts.Repository
{
    public class BankAccountRepository: IBankAccountRepository
    {
        private readonly MiniBankDbContext _context;
        private readonly IMapper _mapper;

        public BankAccountRepository(IMapper mapper, MiniBankDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task Create(Account newAccount)
        {
            var dbAccount = _mapper.Map<Account, AccountDbModel>(newAccount);
            dbAccount.Id=Guid.NewGuid();
            dbAccount.OpeningDate = DateTime.UtcNow;
            await _context.Accounts.AddAsync(dbAccount);
        }

        public async Task<Account> GetAccount(Guid accountId)
        {
            var dbAccount = await _context.Accounts.FirstOrDefaultAsync(it => it.Id == accountId);

            if (dbAccount==null)
                throw new ValidationException("Такого аккаунта не существует");
            
            var coreAccount=_mapper.Map<AccountDbModel, Account>(dbAccount);

            return coreAccount;
        }

        public async Task CloseAccount(Guid id)
        {
            var dbAccount = await _context.Accounts.FirstOrDefaultAsync(it => it.Id == id);
            if (dbAccount is null)
                throw new ValidationException("Аккаунта с таким id не существует");

            if (dbAccount.Sum!=0)
                throw new ValidationException("Не удалось закрыть аккаунт,так как на счету имеются средства");
            
            dbAccount.IsActive = false;
            dbAccount.ClosingDate=DateTime.UtcNow;
        }

        public async Task ChangeAmounts(Account fromAccount, Account toAccount)
        {
            var fromAccountDb =await _context.Accounts.FirstOrDefaultAsync(it => it.Id == fromAccount.Id);
            var toAccountDb = await _context.Accounts.FirstOrDefaultAsync(it => it.Id == toAccount.Id);

            fromAccountDb.Sum = fromAccount.Sum;
            toAccountDb.Sum = toAccount.Sum;
        }


        public async Task<bool> AccountExists(Guid userId)
        {
            var isExists = await _context.Accounts
                .AsNoTracking()
                .AnyAsync(it => it.UserId == userId);
            if (isExists)
                return true;
            return false;
        }

        public async Task<IEnumerable<Account>> GetAllAccounts()
        {
            var accounts = await _context.Accounts
                .AsNoTracking()
                .ToListAsync();
            return _mapper.Map<List<AccountDbModel>, IEnumerable<Account>>(accounts);
        }
    }
}