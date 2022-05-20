using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniBank.Core.Domains.BankAccounts;
using MiniBank.Core.Domains.BankAccounts.Services;
using MiniBank.Web.Controllers.Accounts.Dto;

namespace MiniBank.Web.Controllers.Accounts
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class AccountController: ControllerBase
    {
        private readonly IBankAccountService _bankAccountService;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateAccountDto> _createValidator;

        public AccountController(IBankAccountService bankAccountService, IMapper mapper, IValidator<CreateAccountDto> createValidator)
        {
            _bankAccountService = bankAccountService;
            _mapper = mapper;
            _createValidator = createValidator;
        }
        
        [HttpPost("Create")]
        public async Task Create([FromBody] CreateAccountDto accountDto)
        {
            await _createValidator.ValidateAndThrowAsync(accountDto);
            var account = _mapper.Map<CreateAccountDto, Account>(accountDto);
            await _bankAccountService.Create(account);
        }
        
        [HttpPost("CloseAccount")]
        public async Task CloseAccount(Guid id)
        {
            await _bankAccountService.CloseAccount(id);
        }
        
        [HttpGet("CalculateComission")]
        public decimal CalculateComission(decimal sum,Guid fromAccountId,Guid toAccountId)
        {
            return _bankAccountService.CalculateComission(sum,fromAccountId,toAccountId);
        }
        
        [HttpPost("Remittance")]
        public async Task Remittance(decimal sum,Guid fromAccountId,Guid toAccountId)
        {
            await _bankAccountService.Remittance(sum,fromAccountId,toAccountId);
        }
        
        [HttpGet("GetAllAccounts")]
        public async Task<IEnumerable<GetAccountDto>> GetAllAccounts()
        {
            var accounts=await _bankAccountService.GetAllAccounts();
            return _mapper.Map<IEnumerable<Account>, IEnumerable<GetAccountDto>>(accounts);
        }
    }
}