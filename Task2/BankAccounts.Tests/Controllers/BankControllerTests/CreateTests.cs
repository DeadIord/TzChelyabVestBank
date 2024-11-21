using BankAccounts.Application.Interfaces;
using BankAccounts.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BankAccounts.Tests.Controllers.BankControllerTests
{
    public class CreateTests
    {
        private readonly Mock<IAccountService> _mockAccountService;
        private readonly BankController _controller;

        public CreateTests()
        {
            _mockAccountService = new Mock<IAccountService>();
            _controller = new BankController(_mockAccountService.Object);
        }

        [Fact]
        public void Create_Get_ReturnsViewResult()
        {
            var result = _controller.Create();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Create_Post_ValidOwnerName_RedirectsToIndex()
        {
            var ownerName = "Сергей";

            var result = _controller.Create(ownerName);

            _mockAccountService.Verify(s => s.CreateAccount(ownerName), Times.Once);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
        }
    }
}
