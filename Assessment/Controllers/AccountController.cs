using Assessment.DTO;
using Assessment.Models;
using Assessment.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//test
namespace Assessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly List<Account> _accounts = new List<Account>();
        private readonly Context _dbContext;

        public AccountController (Context context)
        {
            _dbContext = context;
        }

        [HttpPost]
        public IActionResult CreateAccount(Account account)
        {
            if (account.Accounts.ToString().Length != 9)
            {
                return BadRequest("El número de cuenta debe tener una longitud de 9 dígitos.");
            }

            bool isAccountExists = _dbContext.Accounts.Any(a => a.Accounts == account.Accounts);
            if (isAccountExists)
            {
                return BadRequest("Ya existe el No. de Cuenta");
            }


            bool isOwnerExists = _dbContext.Accounts.Any(a => a.Owner == account.Owner);
            if (isOwnerExists)
            {
                return BadRequest("Ya existe No. Cliente");
            }

            account.CreateAt = DateTime.Now;
            _dbContext.Accounts.Add(account);
            _dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Accounts ON");
            _dbContext.SaveChanges();
            _dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Accounts OFF");



            return Ok(new AccountDTO { Account = account.Accounts, Balance = account.Balance, Owner = account.Owner });
            

        }


        [HttpGet]
        public IActionResult GetAccounts()
        {
            var accounts = _dbContext.Accounts.Select(a => new AccountDTO
            {
                Account = a.Accounts,
                Balance = a.Balance,
                Owner = a.Owner,
                CreateAt = a.CreateAt
            }).ToList();

            var response = new
            {
                accounts = accounts
            };

            return Ok(response);
        }

        [HttpGet("OwnerId")]
        public IActionResult GetAccountsForClient(long clientId)
        {
            var accounts = _dbContext.Accounts
                .Where(a => a.Owner == clientId)
                .Select(a => new AccountDTO
                {
                    Account = a.Accounts,
                    Balance = a.Balance,
                    Owner = a.Owner,
                    CreateAt = a.CreateAt
                })
                .ToList();

            var response = new
            {
                accounts = accounts
            };



            return Ok(response);
        }
    }
}

