using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GlownyShop.Api.Controllers
{
    [Route("auth")]
    public class AuthController : Controller
    {
        // GET: api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]string valuey)
        {
            return new string[] { "value1", "value2" };
        }
        
    }
}
