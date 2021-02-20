using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transaction.Business.Services;
using Transaction.Data.DTOs;
using Transaction.Models.Core;

namespace Transaction.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Models.Core.Transaction>>> Get()
        {
            var result = await _transactionService.Get();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Core.Transaction>> Get(Guid id)
        {
            var result = await _transactionService.Get(id);
            if (result == null)
                return BadRequest("Record not found!");

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Models.Core.Transaction>> Post([FromBody] Models.Core.Transaction transaction)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid entries!");

            transaction = await _transactionService.Create(transaction);

            return Ok(transaction);
        }

        [HttpGet("[action]/{personId}")]
        public async Task<ActionResult<List<PersonTransactionDTO>>> GetTransactionsByPersonID(Guid personId)
        {
            var result = await _transactionService.GetTransactionsByPersonIDAsync(personId);
            if (result == null)
                return BadRequest("Record not found!");

            return Ok(result);
        }
    }
}