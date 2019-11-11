using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TransactionFramework.Services.Interface;

namespace TransactionAPI.Controllers
{
    using AutoMapper;
    using Models;
    using TransactionAPI.Services;
    using TransactionFramework.Domain;
    using TransactionFramework.Types;

    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;

        public AccountController(ITransactionService transactionService, IIdentityService identityService, IMapper mapper)
        {
            _transactionService = transactionService;
            _identityService = identityService;
            _mapper = mapper;
        }

        [HttpGet("balance")]
        public async Task<IActionResult> Balance()
        {
            var accountNumber = _identityService.GetIdentity().AccountNumber;
            var transactionResult = await _transactionService.Balance(accountNumber);
            return Ok(_mapper.Map<TransactionResultModel>(transactionResult));
        }

        [HttpPost("deposit")]
        public async Task<IActionResult> Deposit([FromBody] TransactionModel accountTransactionModel)
        {
            var accountTransaction = _mapper.Map<AccountTransaction>(accountTransactionModel);
            accountTransaction.TransactionType = TransactionType.Deposit;
            var result = await _transactionService.Deposit(accountTransaction);
            return Created(string.Empty, _mapper.Map<TransactionResultModel>(result));
        }

        [HttpPost("withdraw")]
        public async Task<IActionResult> Withdraw([FromBody] TransactionModel accountTransactionModel)
        {
            var accountTransaction = _mapper.Map<AccountTransaction>(accountTransactionModel);
            accountTransaction.TransactionType = TransactionType.Withdrawal;
            var result = await _transactionService.Withdraw(accountTransaction);
            return Created(string.Empty, _mapper.Map<TransactionResultModel>(result));
        }
    }
}
