using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    class clientRequest
    {
        public string key1;
        public string key2;
    
    }


    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public string Post([FromBody] string value)
        {
            string clientMsg = value;
            clientRequest requestObj = JsonConvert.DeserializeObject<clientRequest>(clientMsg);
            
            //Extract values from the server response
            var key1 = requestObj.key1;
            var key2 = requestObj.key2;
            string serverMsg = "Server response format: JSON\n";
            serverMsg += $"{key1}+{key2}";
            Console.WriteLine(serverMsg);
            var serverMsgDict = new Dictionary<string, string>
               {
                  
                  { "sendError", "No money" },
                  { "serverStatus", "NG" }
               };

            return JsonConvert.SerializeObject(serverMsgDict);
        }


        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
