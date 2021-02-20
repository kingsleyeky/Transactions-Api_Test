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
    public class PersonController : ControllerBase
    {
        private IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Person>>> Get()
        {
            var result = await _personService.Get();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> Get(Guid id)
        {
            var result = await _personService.Get(id);
            if (result == null)
                return BadRequest("Record not found!");

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Person>> Post([FromBody] Person person)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid entries!");

            person = await _personService.Create(person);
            return Ok(person);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Person>> Put([FromBody] Person person, Guid id)
        {
            if (person.ID != id)
                return BadRequest("Invalid record!");

            if (!ModelState.IsValid)
                return BadRequest("Invalid entries!");

            await _personService.Update(person);
            return Ok(person);
        }
    }
}
