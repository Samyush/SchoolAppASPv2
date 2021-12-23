using Microsoft.AspNetCore.Mvc;
using SchoolAppASPv2.Application.RequestModel;
using SchoolAppASPv2.Core.Entities;
using SchoolAppASPv2.Infastructure.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<Events> Post([FromBody] EventModel model)
        {
            if (ModelState.IsValid)
            {
                var eventData = new Events {
                    EventName = model.EventName,
                    EventVenue = model.Venue,
                    EventDateTime = model.EventDate,
                    
                    //Accademic Events are catogerized as 1 and Extra Act are 0
                    EventType = 0,

                    //on event add, it is automatically set to true
                    //EventStatus = model.
                };

                //var result = await new SchoolEventsServices().AddEvents(eventData);
            }
        return null;
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
