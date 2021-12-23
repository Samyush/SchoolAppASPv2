using Microsoft.AspNetCore.Mvc;
using SchoolAppASPv2.Application.RequestModel;
using SchoolAppASPv2.Core.Entities;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SchoolAppASPv2.Controllers.SchoolEvents
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolsEventsController : ControllerBase
    {
        // GET: api/<SportsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<SportsController>/5
        [HttpGet("{id}")]
        public Events Get(EventModel model)
        {
            return null;
        }

        // POST api/<SportsController>
        [HttpPost]
        public Events Post([FromBody] EventModel model)
        {
            return null ;
        }

        // PUT api/<SportsController>/5
        [HttpPut("{id}")]
        public Events Put(int id, [FromBody] EventModel model)
        {
            return null;;
        }

        // DELETE api/<SportsController>/5
        [HttpDelete("{id}")]
        public Events Delete(EventModel model)
        {
            return null;
        }
    }
}
