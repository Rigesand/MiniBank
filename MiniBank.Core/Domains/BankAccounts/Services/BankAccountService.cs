using System;
using System.Collections.Generic;
using MiniBank.Core.Domains.BankAccounts.Repositories;
using MiniBank.Core.Domains.CurrencyConverters.Services;
using MiniBank.Core.Domains.TranslationHistories;
using MiniBank.Core.Domains.TranslationHistories.Services;
using MiniBank.Core.Domains.Users.Repositories;
using MiniBank.Core.Exception;

namespace MiniBank.Core.Domains.BankAccounts.Services
{
    public class BankAccountService:IBankAccountService
    {
        private readonly IBankAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICurrencyConverter _currencyConverter;
        private readonly ITranslationService _translationService;

        private const string Eur = "EUR";
        private const string Usd = "USD";
        private const string Rub = "RUB";
        private const decimal Comission=1.02m;
        private const decimal ComissionSent=0.98m;
        public BankAccountService(IBankAccountRepository accountRepository, IUserRepository userRepository, ICurrencyConverter currencyConverter, ITranslationService translationService)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
            _currencyConverter = currencyConverter;
            _translationService = translationService;
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

            newAccount.Id = Guid.NewGuid().ToString();
            newAccount.IsActive = true;
            newAccount.OpeningDate=DateTime.Now;
            
            _accountRepository.Create(newAccount);
        }

        public void CloseAccount(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ValidationException("Id не может быть пустым или null");
            
            _accountRepository.CloseAccount(id);
        }

        public decimal CalculateComission(decimal sum, string fromAccountId, string toAccountId)
        {
            if (sum <= 0)
                throw new ValidationException("Сумма не может быть отрицательной или равной нулю");
            
            if (string.IsNullOrEmpty(fromAccountId)||string.IsNullOrEmpty(toAccountId))
                throw new ValidationException("Id не может быть пустым или null");
            
            if (string.Equals(fromAccountId,toAccountId))
                return decimal.Round(sum,2,MidpointRounding.ToEven);

            return decimal.Round(sum * Comission,2,MidpointRounding.ToEven);
        }

        public void Remittance(decimal sum, string fromAccountId, string toAccountId)
        {
            if (sum <= 0)
                throw new ValidationException("Сумма не может быть отрицательной или равной нулю");
            
            if (string.IsNullOrEmpty(fromAccountId)||string.IsNullOrEmpty(toAccountId))
                throw new ValidationException("Id не может быть пустым или null");
            

            var fromAccount = _accountRepository.GetAccount(fromAccountId);
            var toAccount = _accountRepository.GetAccount(toAccountId);

            if (fromAccount.Sum<sum)
                throw new ValidationException("Недостаточно средств для перевода");

            if (!fromAccount.IsActive||!toAccount.IsActive)
                throw new ValidationException("Аккаунт не может быть закрытым");
            
            var sumWithComission=CalculateComission(sum,fromAccountId,toAccountId);
            var amountSent = sum;
            if (!string.Equals(fromAccount.Currency ,toAccount.Currency))
                amountSent=_currencyConverter.Convert(sum, fromAccount.Currency, toAccount.Currency);

            if (fromAccount.Sum==sum)
            {
                fromAccount.Sum = 0;
                toAccount.Sum += ComissionSent*amountSent;
            }
            else
            {
                fromAccount.Sum -= sumWithComission; 
                toAccount.Sum += amountSent;
            }

            _translationService.AddTranslationHistory(new TranslationHistory()
            {
                Sum=amountSent,
                Currency = toAccount.Currency,
                FromAccountId = fromAccount.Id,
                ToAccountId = toAccount.Id
            });
            
            _accountRepository.Remittance(fromAccount,toAccount);
        }

        public List<Account> GetAllAccounts()
        {
            return _accountRepository.GetAllAccounts();
        }
    }
}