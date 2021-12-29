using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolAppASPv2.Application.Common.Interface;
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

        private readonly ISchoolEventsService _services;

        public SchoolsEventsController(ISchoolEventsService services)
        {
            this._services = services;
        }
        // GET: api/<SportsController>
        //[Authorize]
        [HttpGet]
        public ActionResult<dynamic> Get()
        {
            //return new Events[] { "value1", "value2" };
            var result = _services.GetEvents();
            return Ok(result);
        }

        // GET api/<SportsController>/5
        [HttpGet("{id}")]
        public Events Get(int id)
        {
            return null;
        }

        // POST api/<SportsController>
        [HttpPost]
        public async Task<ActionResult<Events>> Post([FromBody] Events model)
        {
            if (ModelState.IsValid)
            {
                var eventData = new Events {
                    //Id = model.Id,
                    EventName = model.EventName,
                    EventVenue = model.EventVenue,
                    EventDateTime = model.EventDateTime,
                    EventStatus = model.EventStatus,
                    
                    //Accademic Events are catogerized as 1 and Extra Act are 0
                    EventType = model.EventType,

                    //on event add, it is automatically set to true
                    //EventStatus = model.
                };

                //why the data cant be passes as below??
                //SchoolEventsServices schoolService = new SchoolEventsServices();
                var result = await _services.AddEvents(eventData);
                if(result != null)
                {

                    return Ok(result);
                }
            }
            //return NotFound();
            return Ok("success");
        }

        // PUT api/<SportsController>/5
        [HttpPut("{id}")]
        public dynamic Put(int id, [FromBody] Events model)
        {
            var result = _services.UpdateEvents(model);
            return result; ;
        }

        // DELETE api/<SportsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Events>> Delete(int id)
        {
            var result = await _services.DeleteEventsAsync(id);
            if(result != null)
            {
                return Ok("success");
            }
            return null;
        }
    }
}
