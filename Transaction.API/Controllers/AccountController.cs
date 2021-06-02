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

        [HttpGet]
        [Route("{AllAccount}")]
        public async Task<ActionResult> Get()
        {
            var result = await _account.GetallAccount();
            return Ok(result);
        }

        [HttpGet("account/{Id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            var result = await _account.GetAccountById(id);
            if (result == null)
                return BadRequest("Record not found!");

            return Ok(result);



        }



        [Route("person/{personId}")]
        [HttpGet]
        public async Task<ActionResult> GetAccountByPersonID(Guid personId)
        {
            var result = await _account.GetAccountByPersonId(personId);
            if (result == null)
                return BadRequest("Record not found!");

            return Ok(result);
        }

        [HttpPost("AddAccount")]
        public async Task<ActionResult> Post([FromBody] Account account)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid entries!");

            await _account.AddAccount(account);
            return Ok(account);
        }

        [HttpPut("UpdateAccount/{Id}")]
        public async Task<IActionResult> UpdateAccount(Account account, Guid Id)
        {

            try
            {

                dynamic result = await _account.UpdateAccount(account, Id);
                if (result.Success == false)
                {
                    return BadRequest(result);
                }
                return Ok(result);

            }
            catch (Exception ex)
            {


                return BadRequest(new { Message = ex.Message + ":" + ex.StackTrace });
            }


        }
    }
}
