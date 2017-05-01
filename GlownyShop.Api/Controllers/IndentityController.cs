using AspNet.Security.OpenIdConnect.Extensions;
using AspNet.Security.OpenIdConnect.Primitives;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GlownyShop.Api.Controllers
{
    [Route("identity")]
    [Authorize]
    public class IndentityController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }

        [Authorize, HttpGet("~/api/test")]
        public IActionResult GetMessage()
        {
            return Json(new
            {
                Subject = User.GetClaim(OpenIdConnectConstants.Claims.Subject),
                Name = User.Identity.Name,
                Roles = from c in User.Claims select new { c.Type, c.Value }
            });
        }
    }
}