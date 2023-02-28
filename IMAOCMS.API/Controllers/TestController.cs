using IMAOCMS.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMAOCMS.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class TestController : CustomBaseController
    {
        private readonly ITestRepository _testRepository;
        public TestController(ITestRepository testRepository)
        {
            _testRepository= testRepository;
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            var  test = await _testRepository.GetListAsync();
            if (test.Success)
            {
                return Ok(test);
            }
            return BadRequest(test.Message);
        }
    }
}
