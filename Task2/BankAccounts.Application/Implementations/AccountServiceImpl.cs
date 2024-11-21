using BankAccount.Core.Models;
using BankAccount.Core.Models.Enums;
using BankAccounts.Application.Interfaces;

namespace BankAccounts.Application.Implementations
{
    public class AccountServiceImpl : IAccountService
    {
        private readonly IBankService _bankService;

        public AccountServiceImpl(IBankService bankService)
        {
            _bankService = bankService;
        }

        public Account CreateAccount(string ownerName)
        {
            var account = new Account { OwnerName = ownerName, Balance = 0 };
            _bankService.AddAccount(account);
            return account;
        }

        public void Deposit(Guid accountId, decimal amount)
        {
            var account = _bankService.GetAccount(accountId);
            if (account == null)
                throw new ArgumentException("Счет не найден.");

            account.Balance += amount;
            AddTransaction(account, amount, TransactionType.Deposit);
        }

        public void Withdraw(Guid accountId, decimal amount)
        {
            var account = _bankService.GetAccount(accountId);
            if (account == null)
                throw new ArgumentException("Счет не найден.");

            if (account.Balance < amount)
                throw new InvalidOperationException("Недостаточно средств на счете.");

            account.Balance -= amount;
            AddTransaction(account, -amount, TransactionType.Withdrawal);
        }

        public void Transfer(Guid fromAccountId, Guid toAccountId, decimal amount)
        {
            var fromAccount = _bankService.GetAccount(fromAccountId);
            var toAccount = _bankService.GetAccount(toAccountId);

            if (fromAccount == null || toAccount == null)
                throw new ArgumentException("Один из счетов не найден.");

            if (fromAccount.Balance < amount)
                throw new InvalidOperationException("Недостаточно средств на счете отправителя.");

            fromAccount.Balance -= amount;
            toAccount.Balance += amount;

            AddTransaction(fromAccount, -amount, TransactionType.Transfer);
            AddTransaction(toAccount, amount, TransactionType.Transfer);
        }

        public Account GetAccount(Guid accountId)
        {
            return _bankService.GetAccount(accountId);
        }

        public IEnumerable<Transaction> GetTransactions(Guid accountId)
        {
            var account = _bankService.GetAccount(accountId);
            return account?.Transactions;
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            return _bankService.GetAllAccounts();
        }

        private void AddTransaction(Account account, decimal amount, TransactionType type)
        {
            var transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                AccountId = account.Id,
                Amount = amount,
                Date = DateTime.Now,
                Type = type
            };
            account.Transactions.Add(transaction);
        }
    }
}
