using IMAOCMS.API.Business.Interfaces;
using IMAOCMS.Core.Common.Responses;
using IMAOCMS.Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace IMAOCMS.API.Controllers;

[Route("/api/[action]")]
public class RFIDDeviceController : CustomBaseController
{
    private readonly IChafone718Service _service;
    public RFIDDeviceController(IChafone718Service service)
    {
        _service = service;
    }
    [HttpPost]
    public async Task<IActionResult> AddDeviceConnectionSettingsDb(RFIDDeviceDto rFIDDeviceDto)
    {
        try
        {
            var result = await _service.AddDeviceConnectionSettingsDb(rFIDDeviceDto);
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
    public async Task<IActionResult> AddDeviceAntennaDb(RFIDDeviceAntennaDto rFIDDeviceAntennaDto)
    {
        try
        {
            var result = await _service.AddDeviceAntennaDb(rFIDDeviceAntennaDto);
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
    public async Task<IActionResult> ConnectionDevice(RFIDDeviceDto rFIDDeviceDto)
    {
        try
        {
            var result = await _service.ConnectionDeviceAsync(rFIDDeviceDto);
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
    [HttpPost]
    public async Task<IActionResult> StartRead()
    {
        try
        {
            var result = await _service.StartRead2Async();
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
    public async Task<IActionResult> StopRead()
    {
        try
        {
            var result = await _service.StopReadAsync();
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
    public async Task<IActionResult> GetRealTime()
    {
        try
        {
            var result = await _service.GetRealTime();
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
    public async Task<IActionResult> GetAntennaPower()
    {
        try
        {
            var result = await _service.GetAntennaPower();
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
    [HttpPut]
    public async Task<IActionResult> SetAntennaPower(RFIDDeviceAntennaDto antennaDto)
    {
        try
        {
            var result = await _service.SetAntennaPower(antennaDto);
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
    public async Task<IActionResult> GetAntennaTemparature()
    {
        try
        {
            var result = await _service.GetTemparature();
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
    public async Task<IActionResult> GetAntennaMeasuringLost([FromQuery]RFIDDeviceMeasuringAPDto measuringAntennaPortsDto)
    {
        try
        {
            var result = await _service.GetAntennaLostDb(measuringAntennaPortsDto);
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
