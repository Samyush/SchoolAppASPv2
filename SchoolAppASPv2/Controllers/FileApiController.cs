using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SchoolAppASPv2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileApiController : ControllerBase
    {
        // GET: api/<FileApiController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<FileApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<FileApiController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<FileApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FileApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
