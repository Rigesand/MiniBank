using System;
using System.Collections.Generic;
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

        private const string Eur = "EUR";
        private const string Usd = "USD";
        private const string Rub = "RUB";
        private const decimal Comission=0.98m;
        public BankAccountService(IBankAccountRepository accountRepository, IUserRepository userRepository, ICurrencyConverter currencyConverter, IRemittanceHistoryService remittanceHistoryService)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
            _currencyConverter = currencyConverter;
            _remittanceHistoryService = remittanceHistoryService;
        }

        public void Create(Account newAccount)
        {
            if (newAccount is null)
                throw new ValidationException("Account не может быть равен null");
            if (newAccount.Currency is not (Eur or Usd or Rub))
                throw new ValidationException("Валюта должна быть одной из RUB,EUR,USD");
            if (newAccount.Sum<0)
                throw new ValidationException("Сумма не может быть отрицательной");
            
            bool isExists=_userRepository.UserExists(newAccount.UserId);

            if (!isExists)
                throw new ValidationException("Пользователя не существует");

            newAccount.Id = Guid.NewGuid();
            newAccount.IsActive = true;
            newAccount.OpeningDate=DateTime.Now;
            
            _accountRepository.Create(newAccount);
        }

        public void CloseAccount(Guid id)
        {
            _accountRepository.CloseAccount(id);
        }

        public decimal CalculateComission(decimal sum, Guid fromAccountId, Guid toAccountId)
        {
            if (sum <= 0)
                throw new ValidationException("Сумма не может быть отрицательной или равной нулю");
            
            
            if (fromAccountId==toAccountId)
                return decimal.Round(sum,2,MidpointRounding.ToEven);

            return decimal.Round(sum * Comission,2,MidpointRounding.ToEven);
        }

        public void Remittance(decimal sum, Guid fromAccountId, Guid toAccountId)
        {
            if (sum <= 0)
                throw new ValidationException("Сумма не может быть отрицательной или равной нулю");
            
            
            var fromAccount = _accountRepository.GetAccount(fromAccountId);
            var toAccount = _accountRepository.GetAccount(toAccountId);

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

            _remittanceHistoryService.AddRemittanceHistory(new RemittanceHistory()
            {
                Sum=amountSent,
                Currency = toAccount.Currency,
                FromAccountId = fromAccount.Id,
                ToAccountId = toAccount.Id
            });
            
            _accountRepository.ChangeAmounts(fromAccount,toAccount);
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            return _accountRepository.GetAllAccounts();
        }
    }
}