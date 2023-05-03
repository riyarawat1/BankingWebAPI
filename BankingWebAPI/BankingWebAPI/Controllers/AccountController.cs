using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;



namespace BankingWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountsController : ControllerBase
    {
        private static readonly List<Account> accounts = new List<Account>
 {
 new Account { AccNo = "100001", AccName = "Riya Rawat", AccType = "Savings", AccBalance = 10000, AccIsActive = true },
 new Account { AccNo = "100002", AccName = "Disha Patani", AccType = "Current", AccBalance = 50000, AccIsActive = true },
 new Account { AccNo = "100003", AccName = "Isha Raman", AccType = "PF", AccBalance = 20000, AccIsActive = false }
 };



        [HttpGet]
        public IEnumerable<Account> GetAllAccounts()
        {
            return accounts;
        }



        [HttpGet("{accNo}")]
        public IActionResult GetAccountByNo(string accNo)
        {
            var account = accounts.FirstOrDefault(a => a.AccNo == accNo);



            if (account == null)
            {
                return NotFound($"Account with account number {accNo} not found");
            }



            return Ok(account);
        }



        [HttpGet("type/{accType}")]
        public IEnumerable<Account> GetAccountsByType(string accType)
        {
            return accounts.Where(a => a.AccType.Equals(accType, StringComparison.OrdinalIgnoreCase));
        }



        [HttpGet("status/{status}")]
        public IEnumerable<Account> GetAccountsByStatus(bool status)
        {
            return accounts.Where(a => a.AccIsActive == status);
        }



        [HttpPost]
        public IActionResult AddAccount(Account newAccount)
        {
            if (accounts.Any(a => a.AccNo == newAccount.AccNo))
            {
                return Conflict($"Account with account number {newAccount.AccNo} already exists");
            }



            accounts.Add(newAccount);



            return CreatedAtAction(nameof(GetAccountByNo), new { accNo = newAccount.AccNo }, newAccount);
        }



        [HttpPut("{accNo}")]
        public IActionResult UpdateAccount(string accNo, Account updatedAccount)
        {
            var account = accounts.FirstOrDefault(a => a.AccNo == accNo);



            if (account == null)
            {
                return NotFound($"Account with account number {accNo} not found");
            }



            account.AccName = updatedAccount.AccName;
            account.AccType = updatedAccount.AccType;
            account.AccBalance = updatedAccount.AccBalance;
            account.AccIsActive = updatedAccount.AccIsActive;



            return NoContent();
        }



        [HttpDelete("{accNo}")]
        public IActionResult DeleteAccount(string accNo)
        {
            var account = accounts.FirstOrDefault(a => a.AccNo == accNo);



            if (account == null)
            {
                return NotFound($"Account with account number {accNo} not found");
            }



            accounts.Remove(account);



            return NoContent();
        }
    }



    public class Account
    {
        public string AccNo { get; set; }
        public string AccName { get; set; }
        public string AccType { get; set; }
        public decimal AccBalance { get; set; }
        public bool AccIsActive { get; set; }
    }
}