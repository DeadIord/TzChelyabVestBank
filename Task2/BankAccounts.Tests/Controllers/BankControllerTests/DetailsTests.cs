using BankAccount.Core.Models;
using BankAccounts.Application.Interfaces;
using BankAccounts.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BankAccounts.Tests.Controllers.BankControllerTests
{
    public class DetailsTests
    {
        private readonly Mock<IAccountService> _mockAccountService;
        private readonly BankController _controller;

        public DetailsTests()
        {
            _mockAccountService = new Mock<IAccountService>();
            _controller = new BankController(_mockAccountService.Object);
        }

        [Fact]
        public void Details_AccountExists_ReturnsViewResult_WithAccount()
        {
            var accountId = Guid.NewGuid();
            var account = new Account { Id = accountId, OwnerName = "Иван", Balance = 100 };
            _mockAccountService.Setup(s => s.GetAccount(accountId)).Returns(account);

            var result = _controller.Details(accountId);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Account>(viewResult.Model);
            Assert.Equal(accountId, model.Id);
        }

        [Fact]
        public void Details_AccountDoesNotExist_ReturnsNotFound()
        {
            var accountId = Guid.NewGuid();
            _mockAccountService.Setup(s => s.GetAccount(accountId)).Returns((Account)null);

            var result = _controller.Details(accountId);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
