using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniBank.Core;
using MiniBank.Core.Domains.BankAccounts.Repositories;
using MiniBank.Core.Domains.CurrencyConverters.Repositories;
using MiniBank.Core.Domains.RemittanceHistories.Repositories;
using MiniBank.Core.Domains.Users.Repositories;
using MiniBank.Data.Accounts.Repository;
using MiniBank.Data.HttpClients.Services;
using MiniBank.Data.RemittanceHistories.Repositories;
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
            services.AddScoped<IRemittanceRepository, RemittanceHistoryRepository>();
            services.AddScoped<IUnitOfWork, EfUnitOfWork>();
            services.AddDbContext<MiniBankDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("MyConnectionString")));

            return services;
        }
    }
}