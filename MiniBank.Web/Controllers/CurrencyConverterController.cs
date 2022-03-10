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
        public decimal Convert(decimal sum,string currencyCode)
        {
            var currencySum=_currencyConverterService.Convert(sum, currencyCode);
            return currencySum;
        }
    }
}