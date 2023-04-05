using IMAOCMS.API.Business.Interfaces;
using IMAOCMS.Core.Common.Responses;
using IMAOCMS.Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace IMAOCMS.API.Controllers;

[Route("api/[controller]/[action]")]
public class RelayCardController : CustomBaseController
{
    private readonly IRelayCardService _relayCardService;

    public RelayCardController(IRelayCardService relayCardService)
    {
        _relayCardService = relayCardService;
    }
    [HttpPost]
    public async Task<IActionResult> ConnectionDevice(RelayCardDeviceDto dto)
    {
        try
        {
            var result = await _relayCardService.ConnectionRelayAsync(dto);
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
    public async Task<IActionResult> DisconnectDevice()
    {
        try
        {
            var result = await _relayCardService.DisconnectRelayAsync();
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
    [HttpPost]
    public async Task<IActionResult> SetOut(string Outs)
    {
        try
        {
            var result = await _relayCardService.SetOut(Outs);
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
    public async Task<IActionResult> ReadStatus()
    {
        try
        {
            var result = await _relayCardService.ReadStatusAsync();
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
