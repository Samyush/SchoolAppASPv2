using System;
using System.Web;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace SchoolAppASPv2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadApiController : ControllerBase
    {
        // GET: api/FileUploadApi
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/FileUploadApi/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/FileUploadApi
        [HttpPost]
        public void Post()
        {
            //string path = HttpContext.Current.Server.MapPath("~/Uploads/");
            string path = null;
            if (!Directory.Exists(path) && path != null)
            {
                Directory.CreateDirectory(path);
            }
            
            //Fetch the file
            //HttpPostedFile 
            
        }

        // PUT: api/FileUploadApi/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/FileUploadApi/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
