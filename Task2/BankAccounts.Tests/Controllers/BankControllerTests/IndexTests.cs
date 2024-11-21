using BankAccount.Core.Models;
using BankAccounts.Application.Interfaces;
using BankAccounts.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BankAccounts.Tests.Controllers.BankControllerTests
{
    public class IndexTests
    {
        private readonly Mock<IAccountService> _mockAccountService;
        private readonly BankController _controller;

        public IndexTests()
        {
            _mockAccountService = new Mock<IAccountService>();
            _controller = new BankController(_mockAccountService.Object);
        }

        [Fact]
        public void Index_ReturnsViewResult_WithListOfAccounts()
        {
            var accounts = new List<Account>
            {
                new Account { Id = Guid.NewGuid(), OwnerName = "Иван", Balance = 100 },
                new Account { Id = Guid.NewGuid(), OwnerName = "Петр", Balance = 200 }
            };
            _mockAccountService.Setup(s => s.GetAllAccounts()).Returns(accounts);

            var result = _controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Account>>(viewResult.Model);
            Assert.Equal(2, model.Count());
        }
    }
}
