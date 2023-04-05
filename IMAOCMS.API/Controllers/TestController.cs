using Microsoft.AspNetCore.Mvc;

namespace IMAOCMS.API.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class TestController : Controller
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok("sdf");
        }
    }
}
