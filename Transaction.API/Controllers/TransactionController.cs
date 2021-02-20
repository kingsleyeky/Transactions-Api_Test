using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transaction.Business.Services;
using Transaction.Data.DTOs;

namespace Transaction.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : BaseController
    {
        private ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService, ILogger logger) : base(logger)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Models.Core.Transaction>>> Get()
        {
            return await ExecuteRequestAsync(async () =>
            {
                var result = await _transactionService.Get();
                return result;
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Core.Transaction>> Get(Guid id)
        {
            return await ExecuteRequestAsync(async () =>
            {
                var result = await _transactionService.Get(id);
                if (result == null)
                    throw new ApplicationException("Record not found!");
                return result;
            });
        }

        [HttpPost]
        public async Task<ActionResult<Models.Core.Transaction>> Post([FromBody] Models.Core.Transaction transaction)
        {
            return await ExecuteRequestAsync(async () =>
            {
                if (!ModelState.IsValid)
                    throw new ApplicationException("Invalid entries!");
                transaction = await _transactionService.Create(transaction);

                return transaction;
            });
        }

        [HttpGet("[action]/{personId}")]
        public async Task<ActionResult<List<PersonTransactionDTO>>> GetTransactionsByPersonID(Guid personId)
        {
            return await ExecuteRequestAsync(async () =>
            {
                var result = await _transactionService.GetTransactionsByPersonIDAsync(personId);
                if (result == null)
                    throw new ApplicationException("Record not found!");

                return result;
            });
        }
    }
}