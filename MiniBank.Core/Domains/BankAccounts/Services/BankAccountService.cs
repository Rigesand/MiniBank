using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MiniBank.Core.Domains.BankAccounts.Repositories;
using MiniBank.Core.Domains.CurrencyConverters.Services;
using MiniBank.Core.Domains.RemittanceHistories;
using MiniBank.Core.Domains.RemittanceHistories.Services;
using MiniBank.Core.Domains.Users.Repositories;
using MiniBank.Core.Exception;

namespace MiniBank.Core.Domains.BankAccounts.Services
{
    public class BankAccountService:IBankAccountService
    {
        private readonly IBankAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICurrencyConverter _currencyConverter;
        private readonly IRemittanceHistoryService _remittanceHistoryService;
        private readonly IUnitOfWork _unitOfWork;

        private const string Eur = "EUR";
        private const string Usd = "USD";
        private const string Rub = "RUB";
        private const decimal Comission=0.98m;
        public BankAccountService(IBankAccountRepository accountRepository, IUserRepository userRepository, ICurrencyConverter currencyConverter, IRemittanceHistoryService remittanceHistoryService, IUnitOfWork unitOfWork)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
            _currencyConverter = currencyConverter;
            _remittanceHistoryService = remittanceHistoryService;
            _unitOfWork = unitOfWork;
        }

        public async Task Create(Account newAccount)
        {
            if (newAccount.Currency is not (Eur or Usd or Rub))
                throw new ValidationException("Валюта должна быть одной из RUB,EUR,USD");

            bool isExists=await _userRepository.UserExists(newAccount.UserId);

            if (!isExists)
                throw new ValidationException("Пользователя не существует");

            newAccount.Id = Guid.NewGuid();
            newAccount.IsActive = true;
            newAccount.OpeningDate=DateTime.UtcNow;
            
            await _accountRepository.Create(newAccount);
            await _unitOfWork.SaveChanges();
        }

        public async Task CloseAccount(Guid id)
        {
            await _accountRepository.CloseAccount(id);
            await _unitOfWork.SaveChanges();
        }

        public decimal CalculateComission(decimal sum, Guid fromUserId, Guid toUserId)
        {
            if (sum <= 0)
                throw new ValidationException("Сумма не может быть отрицательной или равной нулю");
            
            if (fromUserId==toUserId)
                return decimal.Round(sum,2,MidpointRounding.ToEven);

            return decimal.Round(sum * Comission,2,MidpointRounding.ToEven);
        }

        public async Task Remittance(decimal sum, Guid fromAccountId, Guid toAccountId)
        {
            if (sum <= 0)
                throw new ValidationException("Сумма не может быть отрицательной или равной нулю");
            
            
            var fromAccount = await _accountRepository.GetAccount(fromAccountId);
            var toAccount = await _accountRepository.GetAccount(toAccountId);

            if (fromAccount.Sum<sum)
                throw new ValidationException("Недостаточно средств для перевода");

            if (!fromAccount.IsActive||!toAccount.IsActive)
                throw new ValidationException("Аккаунт не может быть закрытым");
            
            var sumWithComission=CalculateComission(sum,fromAccount.UserId,toAccount.UserId);
            var amountSent = sumWithComission;
            if (!string.Equals(fromAccount.Currency ,toAccount.Currency))
                amountSent=_currencyConverter.Convert(sum, fromAccount.Currency, toAccount.Currency);
            
            fromAccount.Sum -= sum; 
            toAccount.Sum += amountSent;

            await _remittanceHistoryService.AddRemittanceHistory(new RemittanceHistory()
            {
                Sum=amountSent,
                Currency = toAccount.Currency,
                FromAccountId = fromAccount.Id,
                ToAccountId = toAccount.Id
            });
            
            await _accountRepository.ChangeAmounts(fromAccount,toAccount);
            await _unitOfWork.SaveChanges();
        }

        public async Task<IEnumerable<Account>> GetAllAccounts()
        {
            return await _accountRepository.GetAllAccounts();
        }
    }
}