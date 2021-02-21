using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transaction.Entity;
using Transaction.Service;

namespace Transaction.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransaction _tranc;

        public TransactionController(ITransaction tranc)
        {
            _tranc = tranc;
            
        }

        [HttpGet]
        [Route("{AllTransaction}")]
        public async Task<ActionResult> Get()
        {
            var result = await _tranc.GetTransaction();
            return Ok(result);
        }

        [HttpGet("GetTransaction/{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            var result = _tranc.GetTransBypersonID(id);
            if (result == null)
                return BadRequest("Record not found!");

            return Ok(result);
        }

        [HttpPost("AddTransaction")]
        public async Task<ActionResult> Post([FromBody] Transactions transaction)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid entries!");

            await _tranc.AddTransaction(transaction);
            return Ok(transaction);
        }
    }
}
