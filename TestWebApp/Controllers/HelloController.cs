using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebApp.Controllers
{
    public class HelloController : Controller
    {
        public IActionResult Message([FromQuery]string name)
        {
            return Content($"Hello {name}");
        }

        [Route("Hello/Message2/{name}")]
        public IActionResult Message2(string name)
        {
            return Content($"Hello {name}");
        }
    }
}
