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
    public class PersonController : BaseController
    {
        private IPersonService _personService;

        public PersonController(IPersonService personService, ILogger logger) : base(logger)
        {
            _personService = personService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Person>>> Get()
        {
            return await ExecuteRequestAsync(async () =>
            {
                var result = await _personService.Get();
                return result;
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> Get(Guid id)
        {
            return await ExecuteRequestAsync(async () =>
            {
                var result = await _personService.Get(id);
                if (result == null)
                    throw new ApplicationException("Record not found!");
                return result;
            });
        }

        [HttpPost]
        public async Task<ActionResult<Person>> Post([FromBody] Person person)
        {
            return await ExecuteRequestAsync(async () =>
            {
                if (!ModelState.IsValid)
                    throw new ApplicationException("Invalid entries!");
                person = await _personService.Create(person);

                return person;
            });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Person>> Put([FromBody] Person person, Guid id)
        {
            return await ExecuteRequestAsync(async () =>
            {
                if (person.ID != id)
                    throw new ApplicationException("Invalid record!");

                if (!ModelState.IsValid)
                    throw new ApplicationException("Invalid entries!");

                await _personService.Update(person);
                return person;
            });
        }
    }
}