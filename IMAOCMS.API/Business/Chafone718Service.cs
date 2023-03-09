using IMAOCMS.API.Business.Interfaces;
using IMAOCMS.Core.Common.Responses;
using IMAOCMS.Core.Request;

namespace IMAOCMS.API.Business
{
    public class Chafone718Service : IChafone718Service
    {
        public Task<ApiDataResponse<BaseRequest>> ConnectionDeviceAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> DisconnectDeviceAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> StartReadAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> StopReadAsync()
        {
            throw new NotImplementedException();
        }
    }
}
