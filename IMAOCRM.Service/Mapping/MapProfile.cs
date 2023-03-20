using AutoMapper;
using IMAOCMS.Core.DTOs;
using IMAOCMS.Core.Entites;

namespace IMAOCRM.Service.Mapping
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<RFIDDevice, RFIDDeviceDto>().ReverseMap();
            CreateMap<RFIDDevice, RFIDDeviceDto>();

            CreateMap<EpcReadData, EpcReadDataDto>().ReverseMap();
            CreateMap<EpcReadData, EpcReadDataDto>();
        } 
    }
}
