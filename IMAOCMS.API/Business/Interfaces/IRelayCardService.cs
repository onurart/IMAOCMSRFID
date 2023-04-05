using IMAOCMS.Core.Common.Responses;
using IMAOCMS.Core.DTOs;
namespace IMAOCMS.API.Business.Interfaces;
public interface IRelayCardService
{
    Task<ApiResponse> ConnectionRelayAsync(RelayCardDeviceDto deviceDto);
    Task<ApiResponse> DisconnectRelayAsync();
    Task<ApiResponse> SetOut(string Outs);
    Task<ApiDicListResponse> ReadStatusAsync();
}