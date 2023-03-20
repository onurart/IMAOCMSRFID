using IMAOCMS.API.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IMAOCMS.API.Controllers
{
    [Route("/api/[action]")]
    public class RfidReaderController : CustomBaseController
    {
        private readonly IRfidReaderService _service;

        public RfidReaderController(IRfidReaderService fidReaderService)
        {
            _service = fidReaderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEpcReadAll()
        {
            var result = await _service.EpcReadAll();
            if (result.Status)
                return Ok(result);
            else
                return BadRequest(result);
        }
    }
}
