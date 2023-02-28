using IMAOCMS.API.Business.Services.Interfaces;
using IMAOCMS.Core.Common.Responses;
using Microsoft.AspNetCore.Mvc;
namespace IMAOCMS.API.Controllers;
[Route("api/")]
[ApiController]
public class DeviceController : CustomBaseController
{
    private readonly IChafone718Service _service;
    public DeviceController(IChafone718Service service)
    {
        _service = service;
    }
    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> ConnectionDevice()
    {
        try
        {
            var result = await _service.ConnectionDeviceAsync();
            if (result.Status)
                return Ok(result);
            else
                return BadRequest(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse() { Message = "Hata" + " - " + ex.ToString(), Status = false });
        }
    }
    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> DisconnectDevice()
    {
        try
        {
            var result = await _service.DisconnectDeviceAsync();
            if (result.Status)
                return Ok(result);
            else
                return BadRequest(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse() { Message = "Hata" + " - " + ex.ToString(), Status = false });
        }
    }
}