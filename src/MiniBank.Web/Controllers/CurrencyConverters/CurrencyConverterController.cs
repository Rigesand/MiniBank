using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniBank.Core.Domains.CurrencyConverters.Services;

namespace MiniBank.Web.Controllers.CurrencyConverters
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class CurrencyConverterController: ControllerBase
    {
        private readonly ICurrencyConverter _currencyConverterService;

        public CurrencyConverterController(ICurrencyConverter currencyConverterService)
        {
            _currencyConverterService = currencyConverterService;
        }
        
        [HttpGet]
        public decimal Convert(decimal amount,string fromCurrency,string toCurrency)
        {
            return _currencyConverterService.Convert(amount, fromCurrency,toCurrency);
        }
    }
}