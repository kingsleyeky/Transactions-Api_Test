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
    public class AccountController : ControllerBase
    {
        private readonly IAccount _account;

        public AccountController(IAccount account)
        {
            _account = account;
        }

        //[HttpGet]
        //public async Task<ActionResult> Get()
        //{
        //    var result = await _ontext.People.ToListAsync();
        //    return Ok(result);
        //}

        //[HttpGet("{id}")]
        //public async Task<ActionResult> Get(Guid id)
        //{
        //    var result = await _ontext.Accounts.FindAsync(id);
        //    if (result == null)
        //        return BadRequest("Record not found!");

        //    return Ok(result);
        //}

        //[HttpGet("[action]/{personId}")]
        //public async Task<ActionResult> GetAccountByPersonID(Guid personId)
        //{
        //    var result = await _ontext.Accounts.FirstOrDefaultAsync(a => a.PersonID == personId);
        //    if (result == null)
        //        return BadRequest("Record not found!");

        //    return Ok(result);
        //}

        [HttpPost("AddAccount")]
        public async Task<ActionResult> Post([FromBody] Account account)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid entries!");

            await _account.AddAccount(account);
            return Ok(account);
        }

        //[HttpPut("{id}")]
        //public async Task<ActionResult> Put([FromBody] Account account, Guid id)
        //{
        //    if (account.ID != id)
        //        return BadRequest("Invalid record!");

        //    if (!ModelState.IsValid)
        //        return BadRequest("Invalid entries!");

        //    _ontext.Accounts.Update(account);
        //    await _ontext.SaveChangesAsync();

        //    return Ok(account);
        //}
    }
}
