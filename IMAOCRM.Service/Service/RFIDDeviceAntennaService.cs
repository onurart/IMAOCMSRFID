using IMAOCMS.Core.Entites;
using IMAOCMS.Core.Repositories;
using IMAOCMS.Core.Services;
using IMAOCMS.Core.UnitOfWorks;
namespace IMAOCRM.Service.Service
{
    public class RFIDDeviceAntennaService : Services<RFIDDeviceAntenna>, IRFIDDeviceAntennaService
    {
        public RFIDDeviceAntennaService(IGenericRepository<RFIDDeviceAntenna> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}