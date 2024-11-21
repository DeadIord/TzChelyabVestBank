using BankAccount.Core.Models;
using BankAccounts.Application.Interfaces;
using BankAccounts.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BankAccounts.Tests.Controllers.BankControllerTests
{
    public class DepositTests
    {
        private readonly Mock<IAccountService> _mockAccountService;
        private readonly BankController _controller;

        public DepositTests()
        {
            _mockAccountService = new Mock<IAccountService>();
            _controller = new BankController(_mockAccountService.Object);
        }

        [Fact]
        public void Deposit_Get_AccountExists_ReturnsViewResult_WithAccount()
        {
            var accountId = Guid.NewGuid();
            var account = new Account { Id = accountId, OwnerName = "Петр", Balance = 200 };
            _mockAccountService.Setup(s => s.GetAccount(accountId)).Returns(account);

            var result = _controller.Deposit(accountId);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Account>(viewResult.Model);
            Assert.Equal(accountId, model.Id);
        }

        [Fact]
        public void Deposit_Get_AccountDoesNotExist_ReturnsNotFound()
        {
            var accountId = Guid.NewGuid();
            _mockAccountService.Setup(s => s.GetAccount(accountId)).Returns((Account)null);

            var result = _controller.Deposit(accountId);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Deposit_Post_ValidAmount_RedirectsToDetails()
        {
            var accountId = Guid.NewGuid();
            decimal amount = 50;

            var result = _controller.Deposit(accountId, amount);

            _mockAccountService.Verify(s => s.Deposit(accountId, amount), Times.Once);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Details", redirectResult.ActionName);
            Assert.Equal(accountId, redirectResult.RouteValues["id"]);
        }

        [Fact]
        public void Deposit_Post_Exception_AddsModelError_ReturnsViewResult()
        {
            var accountId = Guid.NewGuid();
            decimal amount = 50;
            var account = new Account { Id = accountId, OwnerName = "Петр", Balance = 200 };
            _mockAccountService.Setup(s => s.Deposit(accountId, amount)).Throws(new Exception("Test exception"));
            _mockAccountService.Setup(s => s.GetAccount(accountId)).Returns(account);

            var result = _controller.Deposit(accountId, amount);

            _mockAccountService.Verify(s => s.Deposit(accountId, amount), Times.Once);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.False(_controller.ModelState.IsValid);
            Assert.Equal("Test exception", _controller.ModelState[string.Empty].Errors.First().ErrorMessage);
            var model = Assert.IsAssignableFrom<Account>(viewResult.Model);
            Assert.Equal(accountId, model.Id);
        }
    }
}
