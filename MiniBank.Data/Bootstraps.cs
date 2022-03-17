using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniBank.Core.Domains.BankAccounts.Repositories;
using MiniBank.Core.Domains.CurrencyConverters.Repositories;
using MiniBank.Core.Domains.TranslationHistories.Repositories;
using MiniBank.Core.Domains.Users.Repositories;
using MiniBank.Data.Accounts.Repository;
using MiniBank.Data.HttpClients.Services;
using MiniBank.Data.TranslationHistories.Repositories;
using MiniBank.Data.Users.Repositories;

namespace MiniBank.Data
{
    public static class Bootstraps
    {
        
        public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<ICurrencyRepository, CurrencyDataService>(options =>
            {
                options.BaseAddress = new Uri(configuration["ExchangeRate"]);
            });
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBankAccountRepository,BankAccountRepository>();
            services.AddScoped<ITranslationRepository, TranslationRepository>();
            return services;
        }
    }
}