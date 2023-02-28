using IMAOCMS.Core.Common.Responses;
using IMAOCMS.Core.Request;
using Microsoft.AspNetCore.Http.HttpResults;

namespace IMAOCMS.API.Business.Services.Interfaces
{
    public interface IChafone718Service
    {
        Task<ApiDataResponse<BaseRequest>> ConnectionDeviceAsync();
        Task<ApiResponse> DisconnectDeviceAsync();
    }
}
