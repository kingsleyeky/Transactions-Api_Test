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
    public class PersonController : ControllerBase
    {
        private readonly IPerson _person;

        public PersonController(IPerson person)
        {
            _person = person;
        }

        [HttpGet]
      [Route("AllPerson")]
        public async Task<ActionResult> Get()
        {
            var result = await _person.GetallPerson();
            return Ok(result);
        }

        [HttpGet]
        [Route("AllPerson/{Id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            var result = await _person.GetPersonById(id);
            if (result == null)
                return BadRequest("Record not found!");

            return Ok(result);
        }

        [HttpPost("AddPerson")]
        public async Task<ActionResult> Post([FromBody] Person person)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid entries!");

            await _person.AddPerson(person);
            return Ok(person);
        }

        [HttpPut("UpdatePerson/{Id}")]
        public async Task<IActionResult> UpdatePerson(Person person, Guid Id)
        {

            try
            {

                dynamic result = await _person.UpdatePerson(person, Id);
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
