using System;
using Microsoft.AspNetCore.Mvc;
using MiniBank.Core.Interfaces;

namespace MiniBank.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyConverterController
    {
        private readonly ICurrencyConverter _currencyConverterService;

        public CurrencyConverterController(ICurrencyConverter currencyConverterService)
        {
            _currencyConverterService = currencyConverterService;
        }
        
        [HttpGet]
        public int Convert(int sum,string сurrencyСode)
        {
            var currencySum=_currencyConverterService.Convert(sum, сurrencyСode);
            return currencySum;
        }
    }
}