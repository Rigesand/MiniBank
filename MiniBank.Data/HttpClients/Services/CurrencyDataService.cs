using System;
using System.Net.Http;
using System.Net.Http.Json;
using MiniBank.Core.Domains.CurrencyConverters.Repositories;
using MiniBank.Data.HttpClients.Models;

namespace MiniBank.Data.HttpClients.Services
{
    public class CurrencyDataService: ICurrencyRepository
    {
        private readonly HttpClient _httpClient;
        public CurrencyDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public decimal GetExchangeRate(string currencyCode)
        {
            var response = _httpClient.GetFromJsonAsync<CurrencyRate>("daily_json.js")
                .GetAwaiter().GetResult();
            if (response == null)
                throw new Exception();
            return (int) response.Valute[currencyCode].Value;
        }
    }
}