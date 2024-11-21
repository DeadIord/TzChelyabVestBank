using BankAccount.Core.Models;

namespace BankAccounts.Application.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса управления счетами, предоставляющий операции над счетами
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Создает новый счет с указанным именем владельца
        /// </summary>
        /// <param name="ownerName">Имя владельца счета</param>
        /// <returns>Созданный объект счета</returns>
        Account CreateAccount(string ownerName);

        /// <summary>
        /// Выполняет пополнение указанного счета на заданную сумму
        /// </summary>
        /// <param name="accountId">Идентификатор счета для пополнения</param>
        /// <param name="amount">Сумма пополнения</param>
        void Deposit(Guid accountId, decimal amount);

        /// <summary>
        /// Выполняет снятие средств с указанного счета на заданную сумму
        /// </summary>
        /// <param name="accountId">Идентификатор счета для снятия средств</param>
        /// <param name="amount">Сумма для снятия</param>
        void Withdraw(Guid accountId, decimal amount);

        /// <summary>
        /// Переводит средства с одного счета на другой
        /// </summary>
        /// <param name="fromAccountId">Идентификатор счета отправителя</param>
        /// <param name="toAccountId">Идентификатор счета получателя</param>
        /// <param name="amount">Сумма перевода</param>
        void Transfer(Guid fromAccountId, Guid toAccountId, decimal amount);

        /// <summary>
        /// Получает информацию о счете по его идентификатору
        /// </summary>
        /// <param name="accountId">Идентификатор счета</param>
        /// <returns>Объект счета или null, если счет не найден</returns>
        Account GetAccount(Guid accountId);

        /// <summary>
        /// Получает историю транзакций для указанного счета
        /// </summary>
        /// <param name="accountId">Идентификатор счета</param>
        /// <returns>Коллекция транзакций</returns>
        IEnumerable<Transaction> GetTransactions(Guid accountId);

        /// <summary>
        /// Получает все счета, зарегистрированные в системе
        /// </summary>
        /// <returns>Коллекция всех счетов</returns>
        IEnumerable<Account> GetAllAccounts();
    }
}
