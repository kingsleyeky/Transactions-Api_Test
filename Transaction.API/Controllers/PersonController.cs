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
    public class PersonController : ControllerBase
    {
        private readonly TContext _ontext;

        public PersonController(TContext context)
        {
            _ontext = context;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var result = await _ontext.People.ToListAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            var result = await _ontext.People.FindAsync(id);
            if (result == null)
                return BadRequest("Record not found!");

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Person person)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid entries!");

            await _ontext.People.AddAsync(person);
            return Ok(person);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromBody] Person person, Guid id)
        {
            if(person.ID != id)
                return BadRequest("Invalid record!");

            if (!ModelState.IsValid)
                return BadRequest("Invalid entries!");

            _ontext.People.Update(person);
            await _ontext.SaveChangesAsync();

            return Ok(person);
        }
    }
}
