using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Transaction.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly TContext _ontext;

        public TransactionController(TContext context)
        {
            _ontext = context;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var result = await _ontext.Transactions.ToListAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            var result = await _ontext.Transactions.FindAsync(id);
            if (result == null)
                return BadRequest("Record not found!");

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Transaction transaction)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid entries!");

            await _ontext.Transactions.AddAsync(transaction);
            await _ontext.SaveChangesAsync();

            return Ok(transaction);
        }
    }
}
