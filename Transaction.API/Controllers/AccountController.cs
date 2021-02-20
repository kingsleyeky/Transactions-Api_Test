using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transaction.Business.Services;
using Transaction.Models.Core;

namespace Transaction.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private IAccountService _accountService;

        public AccountController(IAccountService accountService, ILogger logger) : base(logger)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Account>>> Get()
        {
            return await ExecuteRequestAsync(async () =>
            {
                var result = await _accountService.Get();
                return result;
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> Get(Guid id)
        {
            return await ExecuteRequestAsync(async () =>
            {
                var result = await _accountService.Get(id);
                if (result == null)
                    throw new ApplicationException("Record not found!");
                return result;
            });
        }

        [HttpGet("[action]/{personId}")]
        public async Task<ActionResult<Account>> GetAccountByPersonID(Guid personId)
        {
            return await ExecuteRequestAsync(async () =>
           {
               var result = await _accountService.GetAccountByPersonID(personId);

               if (result == null)
                   throw new ApplicationException("Record not found!");
               return result;
           });
        }

        [HttpPost]
        public async Task<ActionResult<Account>> Post([FromBody] Account account)
        {
            return await ExecuteRequestAsync(async () =>
            {
                if (!ModelState.IsValid)
                    throw new ApplicationException("Invalid entries!");
                account = await _accountService.Create(account);

                return account;
            });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Account>> Put([FromBody] Account account, Guid id)
        {
            return await ExecuteRequestAsync(async () =>
            {
                if (account.ID != id)
                    throw new ApplicationException("Invalid record!");

                if (!ModelState.IsValid)
                    throw new ApplicationException("Invalid entries!");

                await _accountService.Update(account);
                return account;
            });
        }
    }
}
