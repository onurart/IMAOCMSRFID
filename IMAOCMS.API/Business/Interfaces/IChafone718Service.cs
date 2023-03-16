using IMAOCMS.Core.Common.Responses;
using IMAOCMS.Core.DTOs;
using IMAOCMS.Core.Entites;
using IMAOCMS.Core.Request;
namespace IMAOCMS.API.Business.Interfaces;
public interface IChafone718Service
{
    Task<ApiResponse> ConnectionDeviceAsync(RFIDDeviceDto rFIDDevice);
    Task<ApiResponse> DisconnectDeviceAsync();
    Task<ApiDataListResponse<RFIDDeviceAntennaDto>> GetAntennaPower();
    Task<ApiResponse> StartReadAsync();
    Task<ApiResponse> StartRead2Async();
    Task<ApiResponse> StopReadAsync();
    Task<ApiResponse> StartEpcReader();
   // Task<ApiResponse> ConStartAndStopAndClose();
    Task<ApiDataResponse<RFIDDevice>> AddDeviceConnectionSettingsDb(RFIDDeviceDto rFIDDevice);
    Task<ApiDataResponse<RFIDDeviceAntenna>> AddDeviceAntennaDb(RFIDDeviceAntennaDto rFIDDeviceAntennaDto);
}