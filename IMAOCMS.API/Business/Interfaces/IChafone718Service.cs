using IMAOCMS.Core.Common.Responses;
using IMAOCMS.Core.Request;
namespace IMAOCMS.API.Business.Interfaces;
public interface IChafone718Service
{
    Task<ApiDataResponse<BaseRequest>> ConnectionDeviceAsync();
    Task<ApiResponse> DisconnectDeviceAsync();
    Task<ApiResponse> StartReadAsync();
    Task<ApiResponse> StopReadAsync();
}