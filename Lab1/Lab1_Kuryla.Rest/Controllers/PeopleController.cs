using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab1_Kuryla.Rest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab1_Kuryla.Rest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {

        //[HttpGet]

        //public IActionResult Get()
        //{

        //    var people = new List<Person>
        //    {
        //        new Person
        //        {
        //            FirstName = "Pavel",
        //            LastName = "Kuryla",
        //            PersonId = 1
        //        }
        //    };
        //    return Ok(people);
        //}

        public PeopleDb db;
        public PeopleController(PeopleDb db)
        {
            this.db = db;
        }

        //[HttpGet]
        //public IActionResult Get()
        //{
        //    var people = db.People.ToList();
        //    return Ok(people);
        //}


        //[HttpPost]
        //public async Task<ActionResult<Person>> Post(Person person)
        //{

        //    db.People.Add(person);
        //    await db.SaveChangesAsync();
        //    return Ok("Added");

        //}

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Put(int id, Person person)
        //{
        //    if (id != person.PersonId)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(person).State = EntityState.Modified;

        //    await db.SaveChangesAsync();

        //    return Ok("Updated");
        //}


        //[HttpGet("{id}")]
        //public async Task<ActionResult<Person>> Get(int id)
        //{
        //    var person = await db.People.FindAsync(id);

        //    if (person == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(person);
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var person = await db.People.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            db.People.Remove(person);
            await db.SaveChangesAsync();

            return Ok("Deleted");
        }


    }
}