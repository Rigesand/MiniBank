using Microsoft.Extensions.DependencyInjection;
using MiniBank.Core.Domains.BankAccounts.Services;
using MiniBank.Core.Domains.CurrencyConverters.Services;
using MiniBank.Core.Domains.TranslationHistories.Services;
using MiniBank.Core.Domains.Users.Services;

namespace MiniBank.Core
{
    public static class Bootstraps
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddScoped<ICurrencyConverter, CurrencyConverterService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBankAccountService, BankAccountService>();
            services.AddScoped<ITranslationService, TranslationService>();
            return services;
        }
    }
}