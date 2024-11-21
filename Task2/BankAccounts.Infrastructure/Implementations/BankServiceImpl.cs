using BankAccount.Core.Models;
using BankAccounts.Application.Interfaces;

namespace BankAccounts.Infrastructure.Implementations
{
    public class BankServiceImpl : IBankService
    {
        private readonly List<Account> _accounts = new List<Account>();

        public void AddAccount(Account account)
        {
            _accounts.Add(account);
        }

        public Account GetAccount(Guid accountId)
        {
            return _accounts.FirstOrDefault(a => a.Id == accountId);
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            return _accounts;
        }
    }
}
