using Microsoft.AspNetCore.Mvc;

namespace IMAOCMS.API.Controllers;

[Route("[action]")]
public class RFIDDeviceController : CustomBaseController
{
    [HttpPost]
    public IActionResult Get()
    {
        return Ok("Tamam");
    }
}
