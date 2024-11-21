using BankAccount.Core.Models;
using BankAccounts.Application.Interfaces;
using BankAccounts.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BankAccounts.Tests.Controllers.BankControllerTests
{
    public class TransactionsTests
    {
        private readonly Mock<IAccountService> _mockAccountService;
        private readonly BankController _controller;

        public TransactionsTests()
        {
            _mockAccountService = new Mock<IAccountService>();
            _controller = new BankController(_mockAccountService.Object);
        }

        [Fact]
        public void Transactions_AccountExists_ReturnsViewResult_WithTransactions()
        {
            var accountId = Guid.NewGuid();
            var transactions = new List<Transaction>
            {
                new Transaction { Id = Guid.NewGuid(), Amount = 100 },
                new Transaction { Id = Guid.NewGuid(), Amount = -50 }
            };
            _mockAccountService.Setup(s => s.GetTransactions(accountId)).Returns(transactions);

            var result = _controller.Transactions(accountId);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Transaction>>(viewResult.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public void Transactions_AccountDoesNotExist_ReturnsNotFound()
        {
            var accountId = Guid.NewGuid();
            _mockAccountService.Setup(s => s.GetTransactions(accountId)).Returns((IEnumerable<Transaction>)null);

            var result = _controller.Transactions(accountId);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
