using BankAccount.Core.Models;

namespace BankAccounts.Application.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса банка для управления счетами
    /// </summary>
    public interface IBankService
    {
        /// <summary>
        /// Добавляет новый счет в систему
        /// </summary>
        /// <param name="account">Счет для добавления</param>
        void AddAccount(Account account);

        /// <summary>
        /// Получает счет по заданному идентификатору
        /// </summary>
        /// <param name="accountId">Идентификатор счета</param>
        /// <returns>Объект счета или null, если счет не найден</returns>
        Account GetAccount(Guid accountId);

        /// <summary>
        /// Получает все счета, зарегистрированные в системе
        /// </summary>
        /// <returns>Коллекция всех счетов</returns>
        IEnumerable<Account> GetAllAccounts();
    }
}
