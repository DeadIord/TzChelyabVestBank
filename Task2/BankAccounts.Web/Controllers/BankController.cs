using BankAccounts.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BankAccounts.Web.Controllers
{
    public class BankController : Controller
    {
        private readonly IAccountService _accountService;

        public BankController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IActionResult Index()
        {
            var accounts = _accountService.GetAllAccounts();
            return View(accounts);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string ownerName)
        {
            _accountService.CreateAccount(ownerName);
            return RedirectToAction("Index");
        }

        public IActionResult Details(Guid id)
        {
            var account = _accountService.GetAccount(id);
            if (account == null)
                return NotFound();

            return View(account);
        }

        [HttpGet]
        public IActionResult Deposit(Guid id)
        {
            var account = _accountService.GetAccount(id);
            if (account == null)
                return NotFound();

            return View(account);
        }

        [HttpPost]
        public IActionResult Deposit(Guid id, decimal amount)
        {
            try
            {
                _accountService.Deposit(id, amount);
                return RedirectToAction("Details", new { id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                var account = _accountService.GetAccount(id);
                return View(account);
            }
        }

        [HttpGet]
        public IActionResult Withdraw(Guid id)
        {
            var account = _accountService.GetAccount(id);
            if (account == null)
                return NotFound();

            return View(account);
        }

        [HttpPost]
        public IActionResult Withdraw(Guid id, decimal amount)
        {
            try
            {
                _accountService.Withdraw(id, amount);
                return RedirectToAction("Details", new { id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                var account = _accountService.GetAccount(id);
                return View(account);
            }
        }

        [HttpGet]
        public IActionResult Transfer()
        {
            ViewBag.Accounts = _accountService.GetAllAccounts();
            return View();
        }

        [HttpPost]
        public IActionResult Transfer(Guid fromAccountId, Guid toAccountId, decimal amount)
        {
            try
            {
                _accountService.Transfer(fromAccountId, toAccountId, amount);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                ViewBag.Accounts = _accountService.GetAllAccounts();
                return View();
            }
        }

        public IActionResult Transactions(Guid id)
        {
            var transactions = _accountService.GetTransactions(id);
            if (transactions == null)
                return NotFound();

            return View(transactions);
        }
    }

}
