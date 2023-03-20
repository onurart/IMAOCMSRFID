using IMAOCMS.Core.Common.Responses;
using IMAOCMS.Core.DTOs;
using IMAOCMS.Core.Entites;

namespace IMAOCMS.API.Business.Interfaces;
public interface IRfidReaderService
{
    Task<ApiDataListResponse<EpcReadData>> EpcReadAll();
}