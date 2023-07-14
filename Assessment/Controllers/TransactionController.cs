using Assessment.DTO;
using Assessment.Models;
using Assessment.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace Assessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {

        private readonly Context _dbContext;

        public TransactionController(Context context)
        {
            _dbContext = context;
        }

        [HttpPost]
        public IActionResult MakeTransfer(TransferDTO transferDto)
        {
            var fromAccount = _dbContext.Accounts.FirstOrDefault(a => a.Accounts == transferDto.FromAccount);
            if (fromAccount == null)
            {
                return BadRequest("La cuenta de origen no existe.");
            }

            var toAccount = _dbContext.Accounts.FirstOrDefault(a => a.Accounts == transferDto.ToAccount);
            if (toAccount == null)
            {
                return BadRequest("La cuenta de destino no existe.");
            }

            if (transferDto.Amount <= 0)
            {
                return BadRequest("El monto de transferencia debe ser mayor que cero.");
            }

            if (fromAccount.Balance < transferDto.Amount)
            {
                return BadRequest("Saldo insuficiente en la cuenta de origen.");
            }

            fromAccount.Balance -= transferDto.Amount;
            toAccount.Balance += transferDto.Amount;

            if (fromAccount.Balance < 0 || toAccount.Balance < 0)
            {
                
                fromAccount.Balance += transferDto.Amount;
                toAccount.Balance -= transferDto.Amount;
                return BadRequest("No se puede transferir el monto X saldo Insuficiente");
            }

            _dbContext.SaveChanges();

            var transaction = new Transactions
            {
                
                FromAccount = transferDto.FromAccount,
                ToAccount = transferDto.ToAccount,
                Amount = transferDto.Amount,
                SentAt = DateTime.Now
            };

            _dbContext.Transactions.Add(transaction);
            _dbContext.SaveChanges();

            return Ok("Transferencia realizada exitosamente.");
        }

        [HttpGet("owner_transfers")]
        public IActionResult GetTransactionsForAccount(int accountId)
        {
            var transactions = _dbContext.Transactions
                .Where(t => t.FromAccount == accountId || t.ToAccount == accountId)
                .Select(t => new TransferDTO
                {
                    TransactionId = t.Id,
                    FromAccount = t.FromAccount,
                    ToAccount = t.ToAccount,
                    Amount = t.Amount,
                    TransactionDate = t.SentAt
                })
                .ToList();

            var response = new
            {
                transactions = transactions
            };

            return Ok(response);
        }

        [HttpGet ("owner")]
        public IActionResult GetReceivedTransactionsForAccount(int accountId)
        {
            var receivedTransactions = _dbContext.Transactions
                .Where(t => t.ToAccount == accountId)
                .Select(t => new TransferDTO
                {
                    TransactionId = t.Id,
                    FromAccount = t.FromAccount,
                    Amount = t.Amount,
                    TransactionDate = t.SentAt
                })
                .ToList();

            var response = new
            {
                receivedTransactions = receivedTransactions
            };

            return Ok(response);
        }
        [HttpGet("{accountId}/balance")]
        public IActionResult GetAccountBalance(int accountId)
        {
            var account = _dbContext.Accounts.FirstOrDefault(a => a.Accounts == accountId);
            if (account == null)
            {
                return NotFound("La cuenta no existe.");
            }

            var balance = new AccountDTO
            {
                Account = account.Accounts,
                Balance = account.Balance,
                Owner = account.Owner,
                CreateAt = account.CreateAt,
            };

            var response = new
            {
                balance = balance
            };

            return Ok(response);
        }
    }
}
