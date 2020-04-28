
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Controllers
{
    public class StatusController : Controller
    {
        // GET /status -> 200 Ok
        
        [HttpGet("/status")]
        public ActionResult GetTheStatus()
        {
            var response = new GetStatusResponse
            {
                Message = "Everything is golden!",
                CheckedBy = "Joe Schmidt",
                WhenLastChecked = DateTime.Now
            };
            return Ok(response);
            // one last thing!
        }

    }

    public class GetStatusResponse
    {
        public string Message { get;  set; }
        public string CheckedBy { get; set; }
        public DateTime WhenLastChecked { get; set; }
    }
}
