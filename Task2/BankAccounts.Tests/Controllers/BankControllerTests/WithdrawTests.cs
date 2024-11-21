using BankAccount.Core.Models;
using BankAccounts.Application.Interfaces;
using BankAccounts.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BankAccounts.Tests.Controllers.BankControllerTests
{
    public class WithdrawTests
    {
        private readonly Mock<IAccountService> _mockAccountService;
        private readonly BankController _controller;

        public WithdrawTests()
        {
            _mockAccountService = new Mock<IAccountService>();
            _controller = new BankController(_mockAccountService.Object);
        }

        [Fact]
        public void Withdraw_Get_AccountExists_ReturnsViewResult_WithAccount()
        {
            var accountId = Guid.NewGuid();
            var account = new Account { Id = accountId, OwnerName = "Иван", Balance = 150 };
            _mockAccountService.Setup(s => s.GetAccount(accountId)).Returns(account);

            var result = _controller.Withdraw(accountId);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Account>(viewResult.Model);
            Assert.Equal(accountId, model.Id);
        }

        [Fact]
        public void Withdraw_Get_AccountDoesNotExist_ReturnsNotFound()
        {
            var accountId = Guid.NewGuid();
            _mockAccountService.Setup(s => s.GetAccount(accountId)).Returns((Account)null);

            var result = _controller.Withdraw(accountId);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Withdraw_Post_ValidAmount_RedirectsToDetails()
        {
            var accountId = Guid.NewGuid();
            decimal amount = 50;

            var result = _controller.Withdraw(accountId, amount);

            _mockAccountService.Verify(s => s.Withdraw(accountId, amount), Times.Once);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Details", redirectResult.ActionName);
            Assert.Equal(accountId, redirectResult.RouteValues["id"]);
        }

        [Fact]
        public void Withdraw_Post_Exception_AddsModelError_ReturnsViewResult()
        {
            var accountId = Guid.NewGuid();
            decimal amount = 50;
            var account = new Account { Id = accountId, OwnerName = "Иван", Balance = 150 };
            _mockAccountService.Setup(s => s.Withdraw(accountId, amount)).Throws(new Exception("Test exception"));
            _mockAccountService.Setup(s => s.GetAccount(accountId)).Returns(account);

            var result = _controller.Withdraw(accountId, amount);

            _mockAccountService.Verify(s => s.Withdraw(accountId, amount), Times.Once);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.False(_controller.ModelState.IsValid);
            Assert.Equal("Test exception", _controller.ModelState[string.Empty].Errors.First().ErrorMessage);
            var model = Assert.IsAssignableFrom<Account>(viewResult.Model);
            Assert.Equal(accountId, model.Id);
        }
    }
}
