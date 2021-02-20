using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transaction.Business.Services;
using Transaction.Models.Core;

namespace Transaction.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Account>>> Get()
        {
            var result = await _accountService.Get();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> Get(Guid id)
        {
            var result = await _accountService.Get(id);
            if (result == null)
                return BadRequest("Record not found!");

            return Ok(result);
        }

        [HttpGet("[action]/{personId}")]
        public async Task<ActionResult<Account>> GetAccountByPersonID(Guid personId)
        {
            var result = await _accountService.GetAccountByPersonID(personId);
            if (result == null)
                return BadRequest("Record not found!");

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Account>> Post([FromBody] Account account)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid entries!");

            account = await _accountService.Create(account);
            return Ok(account);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Account>> Put([FromBody] Account account, Guid id)
        {
            if (account.ID != id)
                return BadRequest("Invalid record!");

            if (!ModelState.IsValid)
                return BadRequest("Invalid entries!");

            await _accountService.Update(account);
            return Ok(account);
        }
    }
}
