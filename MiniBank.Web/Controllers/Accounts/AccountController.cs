using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MiniBank.Core.Domains.BankAccounts;
using MiniBank.Core.Domains.BankAccounts.Services;
using MiniBank.Web.Controllers.Accounts.Dto;

namespace MiniBank.Web.Controllers.Accounts
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController: ControllerBase
    {
        private readonly IBankAccountService _bankAccountService;
        private readonly IMapper _mapper;

        public AccountController(IBankAccountService bankAccountService, IMapper mapper)
        {
            _bankAccountService = bankAccountService;
            _mapper = mapper;
        }
        
        [HttpPost("Create")]
        public void Create([FromBody] CreateAccountDto accountDto)
        {
            var account = _mapper.Map<CreateAccountDto, Account>(accountDto);
            _bankAccountService.Create(account);
        }
        
        [HttpPost("CloseAccount")]
        public void CloseAccount(string id)
        {
            _bankAccountService.CloseAccount(id);
        }
        
        [HttpGet("CalculateComission")]
        public decimal CalculateComission(decimal sum,string fromAccountId,string toAccountId)
        {
            return _bankAccountService.CalculateComission(sum,fromAccountId,toAccountId);
        }
        
        [HttpGet("Remittance")]
        public void Remittance(decimal sum,string fromAccountId,string toAccountId)
        {
            _bankAccountService.Remittance(sum,fromAccountId,toAccountId);
        }
        
        [HttpGet("GetAllAccounts")]
        public List<GetAccountDto> GetAllAccounts()
        {
            var accounts=_bankAccountService.GetAllAccounts();
            return _mapper.Map<List<Account>, List<GetAccountDto>>(accounts);
        }
    }
}