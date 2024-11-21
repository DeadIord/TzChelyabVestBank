using BankAccounts.Application.Implementations;
using BankAccounts.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace UniCabinet.Infrastructure
{
    public class ApplicationsServicesExtensions
    {
        public static void AddApplicationLayer(IServiceCollection services)
        {
            services.AddSingleton<IBankService, BankServiceImpl>();
            services.AddTransient<IAccountService, AccountServiceImpl>();

        }
    }
}
