using BankAccount.Core.Models;
using BankAccounts.Application.Interfaces;
using BankAccounts.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BankAccounts.Tests.Controllers.BankControllerTests
{
    public class TransferTests
    {
        private readonly Mock<IAccountService> _mockAccountService;
        private readonly BankController _controller;

        public TransferTests()
        {
            _mockAccountService = new Mock<IAccountService>();
            _controller = new BankController(_mockAccountService.Object);
        }

        [Fact]
        public void Transfer_Get_ReturnsViewResult_WithAccounts()
        {
            var accounts = new List<Account>
            {
                new Account { Id = Guid.NewGuid(), OwnerName = "Иван" },
                new Account { Id = Guid.NewGuid(), OwnerName = "Петр" }
            };
            _mockAccountService.Setup(s => s.GetAllAccounts()).Returns(accounts);

            var result = _controller.Transfer();

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.True(viewResult.ViewData.ContainsKey("Accounts"));
            var model = viewResult.ViewData["Accounts"] as IEnumerable<Account>;
            Assert.NotNull(model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public void Transfer_Post_ValidData_RedirectsToIndex()
        {
            var fromAccountId = Guid.NewGuid();
            var toAccountId = Guid.NewGuid();
            decimal amount = 100;

            var result = _controller.Transfer(fromAccountId, toAccountId, amount);

            _mockAccountService.Verify(s => s.Transfer(fromAccountId, toAccountId, amount), Times.Once);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
        }

        [Fact]
        public void Transfer_Post_Exception_AddsModelError_ReturnsViewResult()
        {
            var fromAccountId = Guid.NewGuid();
            var toAccountId = Guid.NewGuid();
            decimal amount = 100;
            var accounts = new List<Account>
            {
                new Account { Id = fromAccountId, OwnerName = "Иван" },
                new Account { Id = toAccountId, OwnerName = "Петр" }
            };
            _mockAccountService.Setup(s => s.Transfer(fromAccountId, toAccountId, amount)).Throws(new Exception("Test exception"));
            _mockAccountService.Setup(s => s.GetAllAccounts()).Returns(accounts);

            var result = _controller.Transfer(fromAccountId, toAccountId, amount);

            _mockAccountService.Verify(s => s.Transfer(fromAccountId, toAccountId, amount), Times.Once);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.False(_controller.ModelState.IsValid);
            Assert.Equal("Test exception", _controller.ModelState[string.Empty].Errors.First().ErrorMessage);
            Assert.True(viewResult.ViewData.ContainsKey("Accounts"));
            var model = viewResult.ViewData["Accounts"] as IEnumerable<Account>;
            Assert.NotNull(model);
            Assert.Equal(2, model.Count());
        }
    }
}
