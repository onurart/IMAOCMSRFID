using AutoMapper;
using IMAOCMS.API.Business.Interfaces;
using IMAOCMS.Core.CHAFON;
using IMAOCMS.Core.Common.Responses;
using IMAOCMS.Core.DTOs;
using IMAOCMS.Core.Entites;
using IMAOCMS.Core.Services;
using IMAOCRM.Service.Service;
using Mapster;

namespace IMAOCMS.API.Business;
public class RfidReaderService : IRfidReaderService
{
    private readonly ILogger<RfidReaderService> _logger;
    private readonly IMapper _mapper;
    private readonly IEpcReadDataService _epcReadDataService;
    public RfidReaderService(ILogger<RfidReaderService> logger, IEpcReadDataService epcReadDataService, IMapper mapper)
    {
        _logger = logger;
        _epcReadDataService = epcReadDataService;
        _mapper = mapper;
    }



    public async Task<ApiDataListResponse<EpcReadData>> EpcReadAll()
    {
        var resultes = await _epcReadDataService.GetListAsync();
        var epcReadDto = _mapper.Map<List<EpcReadDataDto>>(resultes.Data);
        if (resultes.Success)
        {
            return await Task.FromResult(new ApiDataListResponse<EpcReadData>() { Data =resultes.Data, Message = "Başarılı", Status = true });
        }
        return await Task.FromResult(new ApiDataListResponse<EpcReadData>() { Data = resultes.Data, Message = "Hata", Status = false });

    }
}